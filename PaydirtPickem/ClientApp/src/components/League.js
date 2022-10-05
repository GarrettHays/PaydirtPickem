import React, { Component } from 'react';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import authService from './api-authorization/AuthorizeService';
import './League.css';

export class League extends Component {

    constructor() {
        super();

        this.state = {
            leagueName: ''

        };
        this.handleSubmit = this.handleSubmit.bind(this);
    }


    async handleSubmit(event) {
        event.preventDefault();
        const token = await authService.getAccessToken();
        const data = event.target.league.value;
        fetch(`/api/leagues?leagueName=${data}`, {
            method: 'POST',

            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }
        })

            .then(res => res.json())

            .catch(error => console.error('Error:', error))

            .then(response => console.log('Success:', response));
    }



    render() {
        return (
        <div>
            <img className="teamIMG" src="https://raw.githubusercontent.com/GarrettHays/images/main/LEAGUE.png" alt="logo"></img>
            <Form onSubmit={this.handleSubmit}>
              <FormGroup floating>
                <Input
                  id="leagueName"
                  name="league"
                  placeholder="Enter League Name"
                  type="text"
                />
                <Label for="teamName">
                Enter League Name
                </Label>
              </FormGroup>
              {' '}
              <Button>
                Create League
              </Button>
          </Form>
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
