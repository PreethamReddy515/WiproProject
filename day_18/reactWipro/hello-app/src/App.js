import logo from './logo.svg';
import './App.css';
import First from './components/first/first';
import Second from './components/second/second';
import Third from './components/third/third';
import ButtonEx from './components/buttonEx/buttonEx';
import Four from './components/four/four';
import Five from './components/five/five';
import Six from './components/six/six';
import Seven from './components/seven/seven';
import Eight from './components/eight/eight';

function App() {
  return (
    <div className="App">
      <p> Welcome to React Programming by Neha</p>
      <p>
        <First/>
      </p>
      <p>
        <Second/>
      </p>
      <p>
        <Third firstName="Neha" lastName="Akuthota" company="Wipro"/>
      </p>
      <p>
        <ButtonEx/>
      </p>
      <p>
        <Four/>
      </p>
      <p>
        <Five/>
      </p>
      <p>
        <Six/>
      </p>
      <p>
        <Seven/>
      </p>
      <p>
        <Eight/>
      </p>
    </div>
  );
}

export default App;
