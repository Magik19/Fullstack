import React, { useState , useEffect } from 'react';
import './App.css';
import "bootstrap/dist/css/bootstrap.min.css";
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

function App() {
  const baseUrl=""; //localhost url
  const [data, setData]=useState([]);
  const [modalEditar, setModalEditar]=useState(false);
  const [modalInsertar, setModalInsertar]=useState(false);
  const [modalEliminar, setModalEliminar]=useState(false);
  const [userSeleccionado, setUserSeleccionado]=useState({
    id: "",
    name: "",
    lastname: "",
    email: "",
  })

  const handleChange=e=> {
    const {name, value}=e.target;
    setUserSeleccionado({
      ...userSeleccionado,
      [name]: value
    })
    console.log(userSeleccionado);
  }

  const abrirCerrarModalInsertar=()=>{
    setModalInsertar(!modalInsertar);
  }

  const abrirCerrarModalEditar=()=>{
    setModalEditar(!modalEditar);
  }

  const abrirCerrarModalEliminar=()=>{
    setModalEliminar(!modalEliminar);
  }

  const peticionGet=async()=>{
    await axios.get(baseUrl)
    .then(response=> {
      setData(response.data);
    }).catch(error=>{
      console.log(error);
    })
  }

  const peticionPost=async()=>{
    delete userSeleccionado.id;
    userSeleccionado.email=parseInt(userSeleccionado.email);
    await axios.post(baseUrl, userSeleccionado)
    .then(response=> {
      setData(data.concat(response.data));
      abrirCerrarModalInsertar();
    }).catch(error=>{
      console.log(error);
    })
  }

  const peticionPut=async()=>{
    userSeleccionado.email=parseInt(userSeleccionado.email);
    await axios.put(baseUrl+"/"+userSeleccionado.id, userSeleccionado)
    .then(response=> {
      var respuesta=response.data;
      var dataAuxiliar=data;
      dataAuxiliar.map(user=>{
        if(user.id===userSeleccionado.id){
          user.name=respuesta.name;
          user.lastname=respuesta.lastname;
          user.email=respuesta.email;
        }
      });
      abrirCerrarModalEditar();
    }).catch(error=>{
      console.log(error);
    })
  }

  const peticionDelete=async()=>{
    await axios.delete(baseUrl+"/"+userSeleccionado.id)
    .then(response=> {
      setData(data.filter(user=>user.id!==response.data));
      abrirCerrarModalEliminar();
    }).catch(error=>{
      console.log(error);
    })
  }

  const seleccionarGestor=(user, caso)=>{
    setUserSeleccionado(user);
    (caso==="Editar")?
    abrirCerrarModalEditar(): abrirCerrarModalEliminar();
  }

  useEffect(()=>{
    peticionGet();
  },[])

  return (
    <div className="App">
      <br/><br/>
      <button onClick={()=>abrirCerrarModalInsertar()} className="btn btn-success">Add new User</button>
      <br/><br/>
      <table className='table table-bordered'>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Lastname</th>
            <th>Email</th>
            <th>Options</th>
          </tr>
        </thead>
        <tbody>
        {data.map(user=>(
          <tr key={user.id}>
            <td>{user.id}</td>
            <td>{user.name}</td>
            <td>{user.lastname}</td>
            <td>{user.email}</td>
            <td>
              <button className='btn btn-primary' onClick={()=>seleccionarGestor(user, "Editar")}>Edit</button> {"  "}
              <button className='btn btn-danger' onClick={()=>seleccionarGestor(user, "Eliminar")}>Delete</button>
            </td>
          </tr>
        ))}
        </tbody>

      </table>

          <Modal isOpen={modalInsertar}>
            <ModalHeader>Insertar Gestor de Base de Datos</ModalHeader>
            <ModalBody>
              <div className='form-group'>
                <label>Name:</label>
                <br/>
                <input type="text" className='form-control' name="name" onChange={handleChange}/>
                <br/>
                <label>Lastname:</label>
                <br/>
                <input type="text" className='form-control' name="lastname" onChange={handleChange}/>
                <br/>
                <label>Email:</label>
                <br/>
                <input type="text" className='form-control' name="email" onChange={handleChange}/>
                <br/>
              </div>
            </ModalBody>
            <ModalFooter>
              <button className='btn btn-primary' onClick={()=>peticionPost}>Add</button>{"  "}
              <button className='btn btn-primary'onClick={()=>abrirCerrarModalInsertar}>Cancel</button>
            </ModalFooter>
          </Modal>

          <Modal isOpen={modalEditar}>
            <ModalHeader>Editar Gestor de Base de Datos</ModalHeader>
            <ModalBody>
              <div className='form-group'>
              <label>ID:</label>
                <br/>
                <input type="text" className='form-control' readOnly value={userSeleccionado && userSeleccionado.id}/>
                <br/>
                <label>Name:</label>
                <br/>
                <input type="text" className='form-control' name='name' onChange={handleChange}  value={userSeleccionado && userSeleccionado.name}/>
                <br/>
                <label>Lastname:</label>
                <br/>
                <input type="text" className='form-control' name='lastname' onChange={handleChange}  value={userSeleccionado && userSeleccionado.lastname}/>
                <br/>
                <label>Email:</label>
                <br/>
                <input type="text" className='form-control' name='email' onChange={handleChange}  value={userSeleccionado && userSeleccionado.email}/>
                <br/>
              </div>
            </ModalBody>
            <ModalFooter>
              <button className='btn btn-primary' onClick={()=>peticionPut()}>Edit</button>{"  "}
              <button className='btn btn-primary' onClick={()=>abrirCerrarModalInsertar()}>Cancel</button>
            </ModalFooter>
          </Modal>

          <Modal>
            <ModalBody isOpen={modalEliminar}>
              ¿Estás seguro de que quieres borrar el gesto de la base de datos {userSeleccionado && userSeleccionado.name}?
            </ModalBody>
            <ModalFooter>
              <button className='btn btn-danger' onClick={()=>peticionDelete()}>
                Sí
              </button>
              <button
              className='btn btn-secondary'
              onClick={()=>abrirCerrarModalEliminar()}
              >
                No
              </button>
            </ModalFooter>
          </Modal>

    </div>
  );
}

export default App;