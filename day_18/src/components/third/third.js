import React, {Component} from 'react';
const Third=(props)=>{
  return(
    <div>
      First Name is : <b> {props.firstName} </b><br/>
      Last Name is : <b> {props.lastName} </b><br/>
      Company Name is : <b> {props.company} </b>
    </div>
  )
}

export default Third;