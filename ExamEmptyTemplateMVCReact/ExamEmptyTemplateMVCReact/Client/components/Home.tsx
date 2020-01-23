import * as React from 'react'
import { RouteComponentProps } from "react-router";

export type HomeState = {}
export type HomeProps = {}

export class Home extends React.Component<RouteComponentProps<HomeProps>, HomeState> {
    constructor(props : HomeProps){
        super(props)
        this.state = {}
    }
    
    componentDidMount(){
    }

    public render() {
      return <div className="main">
          It works!    
      </div>
    }
}
