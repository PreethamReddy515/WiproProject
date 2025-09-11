import React, {Component} from 'react';
const ButtonEx=()=>{
  const ButtonOne=()=>{
    alert("You clicked first button");
  }
  const ButtonTwo=()=>{
    alert("You clicked second button");
  }
  const ButtonThree=()=>{
    alert("You clicked third button");
  }
  return(
    <div>
      <input type="button" value="button1" onClick={ButtonOne}/>
      &nbsp;&nbsp;&nbsp;&nbsp;
      <input type="button" value="button2" onClick={ButtonTwo}/>
      &nbsp;&nbsp;&nbsp;&nbsp;
      <input type="button" value="button3" onClick={ButtonThree}/>
    </div>
  )
}

export default ButtonEx;