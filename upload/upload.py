import sys
import pandas as pd
import psycopg2
from psycopg2.extensions import AsIs

# Database connection details
DB_HOST = 'localhost'
DB_PORT = '5432'
DB_NAME = 'OPPO'
DB_USER = 'postgres'
DB_PASSWORD = 'yo1saini'

# Get the filename from command-line arguments
if len(sys.argv) < 2:
    print("Please provide the filename as an argument")
    sys.exit(1)

csv_file_path = sys.argv[1]

# Table name in PostgreSQL
table_name = 'network_data'
def sanitize_column_name(column_name):
    """Sanitize column names to remove spaces and special characters."""
    return column_name.strip().replace(" ", "_").replace("-", "_").lower()

def upload_csv_to_postgres(csv_file_path, table_name):
    try:
        # Connect to PostgreSQL database
        conn = psycopg2.connect(
            host=DB_HOST,
            port=DB_PORT,
            dbname=DB_NAME,
            user=DB_USER,
            password=DB_PASSWORD
        )
        cursor = conn.cursor()

        # Load CSV into pandas DataFrame
        df = pd.read_excel(csv_file_path)  # Use read_excel for .xlsx files
        
        # Sanitize column names
        df.columns = [sanitize_column_name(col) for col in df.columns]

        # Create table dynamically based on the DataFrame
        create_table_query = f"CREATE TABLE IF NOT EXISTS {table_name} ("
        for col in df.columns:
            col_type = 'TEXT'  # Default type
            if pd.api.types.is_integer_dtype(df[col]):
                col_type = 'BIGINT'
            elif pd.api.types.is_float_dtype(df[col]):
                col_type = 'FLOAT'
            create_table_query += f'"{col}" {col_type}, '
        create_table_query = create_table_query.rstrip(", ") + ");"
        cursor.execute(create_table_query)
        conn.commit()

        # Insert data into the table
        for _, row in df.iterrows():
            columns = list(row.index)
            values = [row[col] if pd.notnull(row[col]) else None for col in columns]
            insert_query = f"INSERT INTO {table_name} (%s) VALUES %s"
            cursor.execute(insert_query, (AsIs(','.join(f'"{col}"' for col in columns)), tuple(values)))

        # Commit changes
        conn.commit()
        print(f"Data from {csv_file_path} successfully uploaded to {table_name}.")

    except Exception as e:
        print(f"Error: {e}")
    finally:
        if cursor:
            cursor.close()
        if conn:
            conn.close()

# Run the function
upload_csv_to_postgres(csv_file_path, table_name)

