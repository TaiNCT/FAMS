# store folder

## Summary of State Management

So there is this concept called [State management](https://medium.com/@nouraldin.alsweirki/state-management-in-react-d086459e0bc5) which applies to not only React, but [Vue](https://vuejs.org/) and other frameworks as well, it basically means that somehow we will be able to create some sort of `magical storage`, that any page/view/component can access from anywhere and put/write some data into it. In React, there is this famous way of managing state across multiple contexts and pages by using the [useContext](https://react.dev/reference/react/useContext) hook, but there is also [Redux](https://redux.js.org/) and [Zustand](https://github.com/pmndrs/zustand) which offer better convenience.
<br><br>
Now if you came from a [Vue](https://vuejs.org/) background, then you should be familiar with [Vuex](https://vuex.vuejs.org/), which is the **_Vutified_** way of Zustand or Redux.

## Now why this folder ?

In a straightforward or simple project, there will be only one single `magical storage`, which is usually a single javascript/typescript file using either Redux or Zustand then do some method calling and export itself for other components to use. But in some complex projects, there will be multiple stores, each with its different purposes and what it should be storing.<br>
Hence, this folder exists in case there are multiple stores needed...
