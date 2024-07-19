const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');
const mysql = require('mysql');

const app = express();
const port = 6000;

app.use(cors());
app.use(bodyParser.json());

app.use(bodyParser.json({ limit: '50mb' }));
// Increase the limit to 50mb for URL-encoded payloads
app.use(bodyParser.urlencoded({ limit: '50mb', extended: true }));

//Configuracion de la base de datos
const db = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: '',
    database: 'dbpm02'
});

db.connect((err) => {
    if(err){
        console.log('Error no esta conectado a la base de datos');
        return;
    }
    console.log('Conectado a la base de datos')
});

//Method Create
app.post('/api/empleados', (req, res) => {
    const {nombre, apellido, direccion, foto} = req.body;
    const consulta = 'INSERT INTO EMPLEADOS (nombre, apellido, direccion, foto) VALUES (?, ?, ?, ?)';

    db.query(consulta, [nombre, apellido, direccion, foto], (err,result) => {
        if(err){
            res.status(500).send(err);
            return;
        }
        res.status(200).send(result);
    });
});

//Read
app.get('/api/empleados',(req,res) => {
    const consulta = 'SELECT * FROM empleados';

    db.query(consulta,(err,resultado) => {
        if(err){
            res.status(500).send(err);
            return;
        }
        res.status(200).send(resultado);
    });
});

//Update
app.put('/api/empleados/:id', (req, res) => {
    const { id } = req.params;
    const { nombre, apellido, direccion, foto } = req.body;

    const consulta = 'UPDATE empleados SET nombre = ?, apellido = ?, direccion = ?, foto = ? WHERE id = ?';

    db.query(consulta, [nombre, apellido, direccion, foto, id], (err, result) => {
        if (err) {
            res.status(500).send(err);
            return;
        }
        res.status(200).send(result);
    });
});

//Delete
app.delete('/api/empleados/:id', (req, res) => {
    const { id } = req.params;
    const query = 'DELETE FROM empleados WHERE id = ?';
    db.query(query, [id], (err, result) => {
      if (err) {
        res.status(500).send(err);
        return;
      }
      res.status(200).send(result);
    });
  });


app.listen(port, ()=> {
    console.log(`Servidor ejecutandose en http://192.168.0.10:${port}`);
});
