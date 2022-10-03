import React, { Component } from 'react';
import './Picks.css';
import authService from './api-authorization/AuthorizeService';
import $ from "jquery";

export class Picks extends Component {
  constructor(props) {
    super(props);
    this.state = {
        pickInfo: [],
        form: []
    };
    }

    componentDidMount() {
        this.getPicksFromAPI();
    }

  handleChange = (e) => {
    $('input[name="' + $(this).attr('name') + '"]').not($(this)).trigger('deselect');
    const { form } = this.state;
    this.setState({
      form: {
          ...form,
          [e.target.name]: e.target.value
      }
    });
    console.log('this is changed', this);
  }

  handleClick = async event => {
    event.preventDefault();
    const token = await authService.getAccessToken();
     const data = this.state.form;
        fetch('/api/picks', {
            method: 'POST',

            body: JSON.stringify(data),

            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}`  }

        })
            .catch(error => console.error('Error:', error))
            .then(response => console.log('Success:', response));
    }
  render() {
      let pickInfo = this.state.pickInfo;
      return (
      <div>
        <img id="tabelLabel" className="picksIMG" src="https://raw.githubusercontent.com/GarrettHays/images/main/Picks.png" alt="logo"></img>
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
               {pickInfo.map(pick =>
                  <tr key={pick.homeTeam}>
                      <td><input type='radio' id={pick.homeTeam} name={pick.id} value={pick.homeTeam} onChange={this.handleChange} /></td>
                  <td>{pick.homeTeam}</td>
                  <td>{pick.homeTeamSpread}</td>
                       <td><input type='radio' id={pick.awayTeam} name={pick.id} value={pick.awayTeam} onChange={this.handleChange} /></td>
                  <td>{pick.awayTeam}</td>
                  <td>{(new Date(pick.gameTime)).toLocaleString('en-US')}</td>
                </tr>
              )} 
            </tbody>
            </table>
            <button className='picksButton'onClick={this.handleClick}> Submit Picks </button>
        </form>
      </div>
    );
    }

    async getPicksFromAPI() {
        await fetch('api/picks')
            .then(response => response.json())
            .then(jsonStr => {
                this.setState({ pickInfo: jsonStr });
            })
            .catch(error => console.error('Unable to get items.', error));
    }
}