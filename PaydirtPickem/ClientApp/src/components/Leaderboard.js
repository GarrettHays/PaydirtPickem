import React, { Component } from 'react';
/*import './Leaderboard.css';*/
import authService from './api-authorization/AuthorizeService';
import $ from "jquery";

export class Leaderboard extends Component {
    constructor(props) {
        super(props);
        this.state = {
            scoreInfo: []
        };
    }

    componentDidMount() {
        this.getScoresFromAPI();
    }


    render() {
        let scoreInfo = this.state.scoreInfo;
        return (
            <div>
                <img id="tabelLabel" className="picksIMG" src="https://raw.githubusercontent.com/GarrettHays/images/main/Picks.png" alt="logo"></img>
                <p>Season Standings</p>
                <br />
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Team Name</th>
                            <th>Season Wins</th>
                            <th>Season Losses</th>
                        </tr>
                    </thead>
                    <tbody>
                        {scoreInfo.map(score =>
                            <tr key={score.userId}>
                                <td>{score.teamName}</td>
                                <td>{score.seasonTotalWin }</td>
                                <td>{score.seasonTotalLoss}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    async getScoresFromAPI() {
        const token = await authService.getAccessToken();
        fetch('/api/scores', {
            method: 'GET',

            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }

        })
            .then(response => response.json())
            .then(jsonStr => {
                this.setState({ scoreInfo: jsonStr });
            })
            .catch(error => console.error('Unable to get games.', error));
    }
}