import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';
import './Admin.css';

export class Admin extends Component {
    static displayName = Admin.name;

    handleClick = async event => {
        event.preventDefault();
        const token = await authService.getAccessToken();

        fetch('/api/games', {
            method: 'POST',

            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }

        })

            .catch(error => console.error('Error:', error))

            .then(response => console.log('Success:', response));

        alert("Games Have Been Populated.")
    }

    handleScore = async event => {
        event.preventDefault();
        const token = await authService.getAccessToken();

        fetch('/api/scores', {
            method: 'POST',

            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }

        })

            .catch(error => console.error('Error:', error))

            .then(response => console.log('Success:', response));

        alert("Games Have Been Scored.")
    }

    render() {
        return (
            <div className="adminDIV">
                <img className="adminIMG" src="https://raw.githubusercontent.com/GarrettHays/images/main/ADMIN.png" alt="logo"></img>
                <button className="adminButton" onClick={this.handleClick}> Get Games </button>
          
                <button className="adminButton" onClick={this.handleScore}> Score Games </button>
            </div>
        );
    }
}
