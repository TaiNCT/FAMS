# components folder

## global subfolder :

This one is easy to understand, it stores globally reusable components like customized buttons, drop-down menus, or popups that multiple pages and views reuse over and over again.

## layouts subfolder :

This folder contains what I usually call the "core" components, such as the navbar, the sidebar, and sometimes even the bottom bar... Things that are essential and are always displayed on screen no matter what pages you are at, and people have a term for that, it's called `layouts`, hence the name.

## partial subfolder :

The reason for this folder is... Ideally, to minimize the number of merge conflicts, each team should create a directory in this folder, and each directory will represent a collection of separate components. <br>

- For example : Say, a team of 3 persons who manage all partial views / components relating to `Score Management`, each member with the following tasks...

| person | Task                                  |
| ------ | ------------------------------------- |
| 1      | Create a partial view to update score |
| 2      | Create a view to list scores          |
| 3      | Create a view to list students        |

The entire team will first create a folder here, and then call it whatever they want... Let's say they call it `ScoreManagement`. After that, each member will create a folder inside that `ScoreManagement` folder which stores the Javascript and CSS code of that component, so it is going to be something like this...

```
src
 └ assets
 └ components
    └ global
    └ layouts
    └ partial
      └ ScoreManagement
         └ UpdateScoreComponent
            └ index.tsx
            └ style.module.scss
            └  // other files relating to UpdateScoreComponent
         └ ListScoreComponent
            └ index.tsx
            └ style.module.scss
            └  // other files relating to ListScoreComponent
         └ ListStudentComponent
            └ index.tsx
            └ style.module.scss
            └  // other files relating to ListStudentComponent
 └ styles
 └ App.tsx
 └ main.tsx
 └ vite-env.d.ts
```

So person `1` will manage `components/ScoreManagement/UpdateScoreComponent`, person `2` will manage `components/ScoreManagement/ListScoreComponent` and person `3` will manage `components/ScoreManagement/ListStudentComponent`.

## NOTE :

Please use scoped styling ( or also known as [CSS Modules](https://create-react-app.dev/docs/adding-a-css-modules-stylesheet/) ) to make sure that each component will not poison the global CSS styles by adding any repeated class or id, which may lead to situations like... a button on one page is blue, but when you open it on another page and then return to the old page, it is now red :(
