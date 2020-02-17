import * as React from 'react'
import { RouteComponentProps } from 'react-router';
import * as Models from "./models/model";

//In this example we use an array instead of the list from Immutable package

// Below the Home component which has an empty state and recieves no props
// It is the parent component of the MoviesContainer component
export type HomeState = {}
export type HomeProps = {}

export class Home extends React.Component<RouteComponentProps<HomeProps>, HomeState> {
    constructor(props: HomeProps) {
        super(props)
        this.state = {}
    }

    public render() {
        return <div className="main">
            <MoviesContainer />
        </div>
    }
}


type Movie = Models.Movie;
type Actor = Models.Actor;

// Type declaration for the MoviesContainer component
type MoviesState = { movieList: Movie[] | "Loading", searchMovie: boolean, movieName: string };
type MoviesProps = {}
// The MoviesContainer Component 
export class MoviesContainer extends React.Component<MoviesProps, MoviesState> {
    constructor(props: RouteComponentProps<{ text: string }>) {
        super(props);

        // initializing the state for the movies container 
        this.state = { movieList: "Loading", searchMovie: false, movieName: "" };

        // binding events to functions
        this.renderMovies = this.renderMovies.bind(this)
        this.handleSubmit = this.handleSubmit.bind(this)
        this.renderSearchMovie = this.renderSearchMovie.bind(this)
        this.renderSearchForm = this.renderSearchForm.bind(this)

    }

    // After loading the DOM send a request to the server and handle the response
    // On success save the movies in the state
    // On fail log the error to the console 
    componentDidMount(): void {
        this.loadMovies().then(data => { this.setState({ ...this.state, movieList: data }) })
            .catch(error => console.log(error));
    }


    private loadMovies = async function (): Promise<Movie[]> {
        let res: Response = await fetch("./api/movies", {
            method: "get",
            body: null,
            headers: { "content-type": "application/json" }
        });
        let movies: Promise<Movie[]> = await res.json();
        return movies;
    };

    // Change the state to view or hide the search input field
    private renderSearchMovie(): void {
        this.setState({ ...this.state, searchMovie: !this.state.searchMovie })
        console.log(this.state.searchMovie)
    }

    // Prevent the default behaviour of the browser
    private handleSubmit = (e: React.FormEvent): void => {
        e.preventDefault();
        console.log(e);
    }

    private renderSearchForm(): JSX.Element {
        return (<form onSubmit={this.handleSubmit}>
            <label >Search Movie</label>
            <input type="text" value={this.state.movieName} onChange={(v) => { console.log(this.state.movieName); this.setState({ ...this.state, movieName: v.target.value }) }} ></input>
        </form>);
    }

    // In case any search value is entered, filter the array then render the movies. 
    private renderMovies(ml: Movie[]): JSX.Element {
        return (

            <div>
                {ml.filter(fl => this.state.movieName != "" ? fl.Title.toLowerCase().includes(this.state.movieName.toLowerCase()) : true)
                    .map((m: Movie) => (<MovieRecord key={m.Id} movieProps={m} />))}
            </div>
        );
    }

    public render(): JSX.Element {
        // Render the search input field otherwise render an empty html element
        let addFilterForm = (this.state.searchMovie) ? (this.renderSearchForm()) : (<span></span>);
        // Render loading state otherwise render the movies
        let contents = this.state.movieList == "Loading" ? (<p><em>Loading...</em></p>)
            : (this.renderMovies(this.state.movieList));

        return (
            <div>
                <h1>Movie</h1>
                <button onClick={this.renderSearchMovie}>Search Movies</button>
                {addFilterForm}
                {contents}
            </div>
        );
    }
}

// Type declaration for the MovieRecrod component
type MovieRecordProps = { movieProps: Movie }
type MovieRecordState = {}

// MovieRecord component recieves a movie through the props
export class MovieRecord extends React.Component<MovieRecordProps, MovieRecordState> {
    constructor(props: MovieRecordProps) {
        super(props)
        this.state = {}
    }

    public render() {
        return <div className="main">
            <div>{this.props.movieProps.Title}</div>
            <div>
                {this.props.movieProps.Actors.map((a: Actor) => (<ActorRecord key={a.Id} actorProps={a} />))}
            </div>
        </div>
    }
}

// Type declaration for the ActorRecord component
type ActorRecordProps = { actorProps: Actor }
type ActorRecordState = {}

// ActorRecord component recieves an actor through the props
export class ActorRecord extends React.Component<ActorRecordProps, ActorRecordState> {
    constructor(props: ActorRecordProps) {
        super(props)
        this.state = {}
    }

    public render() {
        return <ul className="main">
            <li>{this.props.actorProps.Name}</li>
            <li>{this.props.actorProps.Gender}</li>
            <li>{this.props.actorProps.Birth}</li>
        </ul>
    }
}