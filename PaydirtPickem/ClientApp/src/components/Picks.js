import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';
import $ from "jquery";

export class Picks extends Component {
  static displayName = Picks.name;

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
        this.handleSubmit = (event) => {
        };
    }

  componentDidMount() {
    this.getPicksFromAPI();
    }

    handleChange = (e) => {
        console.log('handling change!!');
        $('input[name="' + $(this).attr('name') + '"]').not($(this)).trigger('deselect');
        console.log("changing value!!");
        const { form } = this.state;
        this.setState({
            form: {
                ...form,
                [e.target.name]: e.target.value
            }
        });
    }

    handleSubmit(event) {
        console.log("we are in the submit!");

        const data = this.state.form;

        fetch('/api/picks', {
            method: 'POST',

            body: JSON.stringify(data), // data can be `string` or {object}!

            headers: { 'Content-Type': 'application/json' }
        })

            .then(res => res.json())

            .catch(error => console.error('Error:', error))

            .then(response => console.log('Success:', response));
    }

    static renderPicks(pickInfo) {
        return (
            <form onSubmit={this.handleSubmit}>
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
                <button type="submit">Make Picks</button>
        </form>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Picks.renderPicks(this.state.pickInfo);

    return (
      <div>
        <h1 id="tabelLabel">Make Your Picks</h1>
        <p>Select your picks from the games below:</p>
        {contents}
      </div>
    );
  }

    async getPicksFromAPI() {
        const token = await authService.getAccessToken();
        await fetch('api/picks')
            .then(response => response.json())
            .then(jsonStr => {
                this.setState({ pickInfo: jsonStr, loading: false });
            })
            .catch(error => console.error('Unable to get items.', error));
    }
}
