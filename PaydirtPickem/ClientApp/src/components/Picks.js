import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';

export class Picks extends Component {
  static displayName = Picks.name;

  constructor(props) {
    super(props);
    this.state = { pickInfo: [], loading: true };
  }

  componentDidMount() {
    this.getPicksFromAPI();
  }

  static renderPicks(picks) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Home Team</th>
            <th>Home Team Spread</th>
            <th>Away Team</th>
            <th>Game Time</th>
          </tr>
        </thead>
        <tbody>
          {picks.map(pick =>
            <tr key={pick.homeTeam}>
              <td>{pick.homeTeam}</td>
              <td>{pick.homeTeamSpread}</td>
              <td>{pick.awayTeam}</td>
              <td>{pick.gameTime}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Picks.renderPicks(this.state.picks);

    return (
      <div>
        <h1 id="tabelLabel" >Make Your Picks</h1>
        <p>Select your picks from the games below:</p>
        {contents}
      </div>
    );
  }

    async getPicksFromAPI() {
        await fetch('api/picks')
            .then(function (response) {
                return response.json();
            })
            .then(function (jsonData) {
                return JSON.stringify(jsonData);
            })
            .then(jsonStr => {
                this.setState({ pickInfo: jsonStr, loading: false });
                console.log(jsonStr);
            })
            .catch(error => console.error('Unable to get items.', error));
    }
    //    const token = await authService.getAccessToken();
    //    const response = await fetch('picks', {
    //      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    //    });
    //const data = await response.json();
    //this.setState({ picks: data, loading: false });
/*  }*/
}
