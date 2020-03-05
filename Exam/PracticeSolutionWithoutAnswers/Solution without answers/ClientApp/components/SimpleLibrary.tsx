import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import * as Models from '../models'


async function getAllAuthorsAndBooks(): Promise<Models.AuthorBooks[]> {
  //DONE 8: missing code 0.5pt
  let res = await fetch("api/Library/GetAuthorsAndBooks")
  /////
  return res.json()
}

//DONE 9: missing code 1pt
type SimpleLibraryComponentState = {
  AuthorsAndBooks : "loading" | "error" | Models.AuthorBooks[],
  FilterAuthorString : string,
  FilterBookString : string
}
/////

export class SimpleLibraryComponent extends React.Component<RouteComponentProps<{}>, SimpleLibraryComponentState> {
  constructor(props: RouteComponentProps<{}>, context: any) {
    super(props, context);
    this.state = { AuthorsAndBooks: "loading", FilterAuthorString: "", FilterBookString: "" }
  }

  try_download_allAuthorsAndBooks() {
    this.setState({ ...this.state, AuthorsAndBooks: "loading" },
    //DONE 10: missing code 1pt
    () => getAllAuthorsAndBooks()
    .then(ab => this.setState({...this.state, AuthorsAndBooks: ab}))
    .catch(e => this.setState({...this.state, AuthorsAndBooks: "error"}, 
    () => console.log(e)))
    /////
      )
  }
  componentWillMount() {
    this.try_download_allAuthorsAndBooks()
  }

  public render() {
    return <div>{
      this.state.AuthorsAndBooks == "loading" ? <div>loading...</div> :
        this.state.AuthorsAndBooks == "error" ? <div>Something went wrong while downloading...<button onClick={() => 
            //DONE 11: missing code 0.5pt
            this.try_download_allAuthorsAndBooks()
            /////
          }>Retry</button></div> :
          <div>
            <form>
              Search author:
                <input type="text" name="name" value={this.state.FilterAuthorString} onChange={(v) => { this.setState({ ...this.state, FilterAuthorString: v.target.value }) }} />
              <div/>
              Search book:
                <input type="text" name="name" value={this.state.FilterBookString} onChange={(v) => { this.setState({ ...this.state, FilterBookString: v.target.value }) }} />
            </form>
            {this.state.AuthorsAndBooks
              .filter(a_bs => this.state.FilterAuthorString != "" ? a_bs.author.name.toLowerCase().includes(this.state.FilterAuthorString) : true)
              .map(a_bs => <div>
                <h2>Author</h2>
                <AuthorComponent Author={a_bs.author} />
                <h4>Books</h4>
                {a_bs.books
                  //DONE 12: missing code 0.5pt
                  .map(b => <BookComponent Book={b} />)
                  /////
                }

              </div>)}
          </div>
    }
    </div>;
  }
}



type AuthorComponentProps = {
  Author: Models.Author
}
export class AuthorComponent extends React.Component<AuthorComponentProps, {}> {
  constructor(props: AuthorComponentProps, context: any) {
    super(props, context);
    this.state = {}
  }
  public render() {
    return <div>
      {this.props.Author.name},
      {this.props.Author.gender},
      {this.props.Author.birth.toString()},
    </div>;
  }
}

type BookComponentProps = {
  Book: Models.Book
}
export class BookComponent extends React.Component<BookComponentProps, {}> {
  //DONE 13: missing code 0.5pt
  constructor(props: BookComponentProps, context: any) {
    super(props, context);
    this.state = {}
  }
  public render() {
    return <div>
      {this.props.Book.title}, 
      {this.props.Book.year}
    </div>;
  }
  /////
}


