import React, { Component } from 'react';

export class League extends Component {
    static displayName = League.name;

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
                        name='league'
                        placeholder='League Name' /> <br />
                    <button type='submit'>submit</button>
                </form>
            </div>
        );
    }
}
