import React, { Component } from 'react';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import './Team.css';

export class Team extends Component {

    constructor() {
        super();

        this.state = {
            name: '',
            team: '',
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


        const data = { name: this.state.name, team: this.state.team, league: this.state.league }

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
            <img className="teamIMG" src="https://raw.githubusercontent.com/GarrettHays/images/main/TEAM.png" alt="logo"></img>
            <Form onSubmit={this.handleSubmit}>
              <FormGroup floating>
                <Input
                  id="teamName"
                  name="name"
                  placeholder="Enter Your Team Name"
                  type="text"
                />
                <Label for="teamName">
                Enter Your Team Name
                </Label>
              </FormGroup>
              {' '}
              <FormGroup>
                <Input
                  id="exampleSelect"
                  name="select"
                  type="select"
                >
                  <option selected disabled>
                    Select League to Join
                  </option>
                  <option>
                    League 1
                  </option>
                  <option>
                    League 2
                  </option>
                  <option>
                    League 3
                  </option>
                  <option>
                    League 4
                  </option>
                  <option>
                    League 5
                  </option>
                </Input>
              </FormGroup>
              {' '}
              <Button>
                Submit
              </Button>
          </Form>
        </div>
        );
      }
}
      
          
          
          
//           <div>
//             <img className="teamIMG" src="https://raw.githubusercontent.com/GarrettHays/images/main/TEAM.png" alt="logo"></img>
//             <form onSubmit={this.handleSubmit}>
//                 <label htmlFor="name">Enter Name: </label>
//                 <input id="name" name="name" type="text" onChange={this.handleChange} />
//                 <br />
//                 <label htmlFor="name">Enter Team Name: </label>
//                 <input id="name" name="name" type="text" onChange={this.handleChange} />
//                 <br />
//                 <label htmlFor="league">Select League to Join: </label>
//                 <select className="league" id="league" name="league" type="league" onChange={this.handleChange}>
//                     <option value="selected" selected="selected">Available Leagues</option>
//                     <option value="option1" value="option3">League 1</option>
//                     <option value="option2" value="option4">League 2</option>
//                     <option value="option5" value="option6">League 3</option>
//                 </select>  
//                 <br />
//                 <button>Create Team!</button>
//             </form>
//           </div> 
//         );
//     }
// }
