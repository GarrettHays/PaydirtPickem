import React, { Component } from 'react';
import './League.css';

export class League extends Component {

    constructor() {
        super();

        this.state = {
            name: '',
            league: '',

        };
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange = (e) => {
        this.setState({
            [e.target.name]: e.target.value
        })
    }

    handleSubmit(event) {
        event.preventDefault();


        const data = { name: this.state.name, league: this.state.league }

        fetch('/api/createAccount', {
            method: 'POST',

            body: JSON.stringify(data), // data can be `string` or {object}!

            headers: { 'Content-Type': 'application/json' }
        })

            .then(res => res.json())

            .catch(error => console.error('Error:', error))

            .then(response => console.log('Success:', response));
    }



    render() {
        return (
          <div>  
            <img className="leagueIMG" src="https://raw.githubusercontent.com/GarrettHays/images/main/LEAGUE.png" alt="logo"></img>
            <form onSubmit={this.handleSubmit}>
                <label htmlFor="name">Enter Name: </label>
                <input id="name" name="name" type="text" onChange={this.handleChange} />
                <br/>
                <label htmlFor="league">Enter League Name: </label>
                <input id="league" name="league" type="league" onChange={this.handleChange} />
                <br/>
                <button>Create League!</button>
            </form>
          </div>
        );
    }
}




//import React, { Component } from 'react';

//export class League extends Component {
//    static displayName = League.name;

//    render() {
//        return (
//            <div>
//                <form>
//                    <input
//                        type='text'
//                        name='name'
//                        placeholder='Name' /> <br />
//                    <input
//                        type='text'
//                        name='league'
//                        placeholder='League Name' /> <br />
//                    <button type='submit'>submit</button>
//                </form>
//            </div>
//        );
//    }
//}
