import React, { Component } from 'react';
import './Picks.css';
import authService from './api-authorization/AuthorizeService';
import $ from "jquery";

export class Picks extends Component {
  constructor(props) {
    super(props);
    this.state = {
        pickInfo: [],
        userPicks: [],
        form: []
    };
    }

    componentDidMount() {
        this.getGamesFromAPI();
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
    //const gameCount = this.state.pickInfo.length;
    //const selectedCount = Object.keys(this.state.form).length+1;

    //if (gameCount === selectedCount) {
    //    document.getElementById("picksButton").disabled = false;
    //}
  }

  handleClick = async event => {
    /*event.preventDefault();*/
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
      let userPicks = this.state.userPicks;
      let pickInfo = this.state.pickInfo;
      let form;
      if (userPicks === null || userPicks.length === 0) {
          if (pickInfo.length === 0) {
              form =
                  <div>
                      <img id="tabelLabel" className="gamesIMG" src="https://raw.githubusercontent.com/GarrettHays/images/main/games.png" alt="logo"></img>
                      <p>Contact Admin to Populate Games</p>
                  </div>
          }
          else {
              form =
                  <div>
                      <img id="tabelLabel" className="makepicksIMG" src="https://raw.githubusercontent.com/GarrettHays/images/main/Picks.png" alt="logo"></img>
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
                          <button id="picksButton" className='picksButton' onClick={this.handleClick}> Submit Picks </button>
                      </form>
                  </div>;
          } 
      }
      else {
        form =
        <div className="yourPicksView">
                <img id="tabelLabel" className="picksIMG" src="https://raw.githubusercontent.com/GarrettHays/images/main/PickedPicks.png" alt="logo"></img>
                <p><em>Once games are scored, correct picks are green & incorrect picks are red.</em></p>
                <br/>
            <table className='table table-striped' aria-labelledby="tabelLabel">
              <thead>
                <tr>
                  <th>Home Team</th>
                  <th>Home Team Spread</th>
                  <th>Away Team</th>
                  <th>Game Time (User Local Time)</th>
                  <th>Your Pick</th>
                </tr>
              </thead>
              <tbody>
                {userPicks.map(pick =>
                    <tr key={pick.game.homeTeam}>
                    <td>{pick.game.homeTeam}</td>
                    <td>{pick.game.homeTeamSpread}</td>
                    <td>{pick.game.awayTeam}</td>
                    <td>{(new Date(pick.game.gameTime)).toLocaleString('en-US')}</td>
                    <td className={pick.correctPick === null ? "notScored" : (pick.correctPick ? "correctPick" : "wrongPick")}>{pick.pickedTeam}</td>
                  </tr>
                )} 
              </tbody>
              </table>
        </div>;
      }
      return (
        <div>
          { form }
        </div>
        
    );
    }

    async getGamesFromAPI() {
        const token = await authService.getAccessToken();
        fetch('/api/games', {
            method: 'GET',

            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }

        })
            .then(response => response.json())
            .then(jsonStr => {
                this.setState({ pickInfo: jsonStr });
            })
            .catch(error => console.error('Unable to get games.', error));
    }

    async getPicksFromAPI() {
        const token = await authService.getAccessToken();
        fetch('/api/picks', {
            method: 'GET',

            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }

        })
            .then(response => response.json())
            .then(jsonStr => {
                this.setState({ userPicks: jsonStr });
            })
            .catch(error => console.error('Unable to get picks.', error));
    }
}