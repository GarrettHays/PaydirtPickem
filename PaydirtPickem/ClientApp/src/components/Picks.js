import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';

export class Picks extends Component {
  constructor(props) {
    super(props);
    this.state = {
        pickInfo: [],
        loading: true,
        form: {
            name: '',
            value: ''
        }
    };
  }

  handleClick = async event => {
    event.preventDefault();
    console.log('this is:', this);
    const token = await authService.getAccessToken();
    const data = [{ gameId: "7f2ff573-e878-4892-bcab-86aa3c81f7c9", pickedTeam: "Boofaloo Bills" }];

        fetch('/api/picks', {
            method: 'POST',

            body: JSON.stringify(data),

            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`  }

        })

            // .then(res => res.json())

            .catch(error => console.error('Error:', error))

            .then(response => console.log('Success:', response));
    }
  render() {
    return (
      <div>
        <h1 id="tabelLabel">Make Your Picks</h1>
        <p>Select your picks from the games below:</p>
        <form>
          <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
              <tr>
                <th></th>
                <th>Home Team</th>
                <th>Home Team Spread</th>
                <th></th>
                <th>Away Team</th>
                <th>Game Time (User Local Time)</th>
              </tr>
            </thead>
            <tbody>
                <tr key='Buffaloo Bills'>
                      <td><input type='radio' id='Buffaloo Bills' name='Buffaloo Bills' value='Buffaloo Bills' onChange={this.handleChange} /></td>
                  <td>'Buffaloo Bills'</td>
                  <td>5</td>
                      <td><input type='radio' id='Buffaloo Shills' name='Buffaloo Bills' value='Buffaloo Shills' onChange={this.handleChange} /></td>
                  <td>'Buffaloo Shills'</td>
                  <td>{(new Date()).toLocaleString('en-US')}</td>
                </tr>

                <tr key='KC Chefs'>
                      <td><input type='radio' id='KC Chefs' name='KC Chefs' value='KC Chefs' onChange={this.handleChange} /></td>
                  <td>'KC Chefs'</td>
                  <td>5</td>
                      <td><input type='radio' id='KC Chefs' name='KC Chefs' value='KC Chefs' onChange={this.handleChange} /></td>
                  <td>'KC Chefs''</td>
                  <td>{(new Date()).toLocaleString('en-US')}</td>
                </tr>
              
              {/* {pickInfo.map(pick =>
                  <tr key={pick.homeTeam}>
                      <td><input type='radio' id={pick.homeTeam} name={pick.homeTeam} value={pick.homeTeam} onChange={this.handleChange} /></td>
                  <td>{pick.homeTeam}</td>
                  <td>{pick.homeTeamSpread}</td>
                      <td><input type='radio' id={pick.awayTeam} name={pick.homeTeam} value={pick.awayTeam} onChange={this.handleChange} /></td>
                  <td>{pick.awayTeam}</td>
                  <td>{(new Date(pick.gameTime)).toLocaleString('en-US')}</td>
                </tr>
              )} */}
            </tbody>
            </table>
            <button onClick={this.handleClick}> Click me </button>
        </form>
      </div>
    );
  }
}