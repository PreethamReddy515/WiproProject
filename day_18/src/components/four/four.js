import React, {Component, useState} from 'react';
const Four = () => {
  const [name,setName] = useState('')
  const ajay = () => {
    setName("Ajay...");
  }
  const pralavi = () => {
    setName("Pralavi...");
  }

  const uday = () => {
    setName("Uday...");
  }

  return(
    <div>
      <input type="button" value="Ajay" onClick={ajay} />
      &nbsp;&nbsp;&nbsp;&nbsp;
      <input type="button" value="Pralavi" onClick={pralavi} /> 
      &nbsp;&nbsp;&nbsp;&nbsp;
      <input type="button" value="Uday" onClick={uday} />
      <hr/>
      Name is : <b>{name}</b>
    </div>
  )
}
export default Four;