const express = require('express');
const { exec } = require('child_process');
const cors = require('cors');
const app = express();
const port = 3000;

app.use(cors());
app.use(express.json());

app.post('/process-file', (req, res) => {
  const { filename } = req.body;
  
  // Update the Python script path
  const pythonScriptPath = './upload.py';
  
  // Run the Python script
  exec(`python ${pythonScriptPath} ${filename}`, (error, stdout, stderr) => {
    if (error) {
      console.error(`Error: ${error.message}`);
      return res.status(500).json({ message: 'Error processing file' });
    }
    if (stderr) {
      console.error(`stderr: ${stderr}`);
      return res.status(500).json({ message: 'Error processing file' });
    }
    console.log(`stdout: ${stdout}`);
    res.json({ message: 'File processed successfully' });
  });
});

app.listen(port, () => {
  console.log(`Server running at http://localhost:${port}`);
});
