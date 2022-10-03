import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Hello!</h1>
            <p>Welcome NoviAMS client</p>
            <p>This client fetches data from the NoviAMS api https://180930b.novitesting.com/ and renders a listing of the members</p>
            <p>Clicking the individual members launches a modal that renders the details of the individual members which is also fetched from an api call</p>
      </div>
    );
  }
}
