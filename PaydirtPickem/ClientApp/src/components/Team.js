import React, { Component } from 'react';

export class Team extends Component {
    static displayName = Team.name;

    render() {
        return (
            <div>
                <form>
                    <input
                        type='text'
                        name='name'
                        placeholder='Name' /> <br />
                    <input
                        type='text'
                        name='team'
                        placeholder='Team Name' /> <br />
                    <input
                        type='text'
                        name='league'
                        placeholder='Select League to Join' /> <br />
                    <button type='submit'>submit</button>
                </form>
            </div>
        );
    }
}