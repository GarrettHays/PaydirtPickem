import React, { Component } from 'react';
import './Home.css';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
       <div className='homeDIV'>
            <img className='homeIMG' src="https://raw.githubusercontent.com/GarrettHays/images/main/PaydirtLOGONew.png" alt="logo"></img>
            <p>To get started, head over to <strong>register</strong> or <strong>login</strong> page.</p>
            <p>Then, navigate to <strong>picks</strong> to make your selections!</p>
            <p>If you've got questions, the <strong>rules</strong> has answers.</p>
            <br/>
 
        {/*<p>new features:</p>*/}
        {/*<ul>*/}
        {/*  <li><strong>league creation</strong>: users can now create private leagues.</li>*/}
        {/*  <li><strong>individual team creation</strong>: users can now create their own pick'em teams to participate in leagues.</li>*/}
        {/*</ul>*/}
        </div>
    );
  }
}
