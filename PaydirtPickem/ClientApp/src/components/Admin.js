import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';

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
    }

    render() {
        return (
            <div>
                <button onClick={this.handleClick}> Get Week's Game Spreads </button>
            </div>
        );
    }
}
