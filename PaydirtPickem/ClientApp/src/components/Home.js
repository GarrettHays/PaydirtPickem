import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Welcome to Paydirt Pick'em!</h1>
        <p>Gather your friends, create a league, pick against the spread and get that bag.</p>
        <p>To get started, head over to the <strong>create league</strong> page.</p>
 
        <p>New Features:</p>
        <ul>
          <li><strong>League Creation</strong>: Users can now create private leagues.</li>
          <li><strong>Individual Team Creation</strong>: Users can now create their own Pick'em teams to participate in Leagues.</li>
        </ul>
      </div>
    );
  }
}
