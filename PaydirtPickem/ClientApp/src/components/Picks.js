import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';

export class Picks extends Component {
  static displayName = Picks.name;

    constructor(props) {
        super(props);
        this.state = { pickInfo: [], value: 'test', loading: true };
    }
    //  this.handleChange = this.handleChange.bind(this);
    //  this.handleSubmit = this.handleSubmit.bind(this);
    //}

    //handleChange(event) {
    //    console.log('a pick was clicked' + event.target.value);
    //    this.setState({ value: event.target.value });
    //}

    //handleSubmit(event) {
    //    console.log('A pick was submitted: ' + this.state.value);
    //    event.preventDefault();
/*    }*/

  componentDidMount() {
    this.getPicksFromAPI();
  }

    //static  makePick() {
    //    console.log('pick made');
    //}

    static renderPicks(pickInfo) {

        return (
        <form >
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
                      <td><input type='radio' id='{pick.homeTeam}' name='{pick.homeTeam}{pick.awayTeam}' value='1'/></td>
                      {/*<td><input type='radio' id='{pick.homeTeam}' name='{pick.homeTeam}{pick.awayTeam}' value={this.state.value} onChange={this.handleChange} /></td>*/}
                    {/*<td><input type='radio' id='{pick.homeTeam}' name='{pick.homeTeam}{pick.awayTeam}' value='{pick.homeTeam}' /></td>*/}
                  <td>{pick.homeTeam}</td>
                  <td>{pick.homeTeamSpread}</td>
                  <td><input type='radio' id='{pick.awayTeam}' name='{pick.homeTeam}{pick.awayTeam}1' value='{pick.awayTeam}' /></td>
                  <td>{pick.awayTeam}</td>
                  <td>{(new Date(pick.gameTime)).toLocaleString('en-US')}</td>
                </tr>
              )}
            </tbody>
            </table>
                <button onClick="makePick()">Make Picks</button>
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
