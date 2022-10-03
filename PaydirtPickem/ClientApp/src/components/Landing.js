import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Landing extends Component {
  static displayName = Landing.name;

  render() {
    return (
      <div>
        <img src="https://raw.githubusercontent.com/GarrettHays/images/main/PaydirtLOGO.png" alt="logo"></img>
        <button tag={Link} className="" to="/admin">Login</button>
      </div>
    );
  }
}
