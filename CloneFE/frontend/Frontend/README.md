# FAMS - React frontend

Instead of the typical template of any React project, i will make it fast... So for development :

```sh
npm install
npm run dev
```

For production :

```sh
npm install
npm run build # First build and minify CSS and stuffs
npm run preview # Needed to preview the built source code
```

If you wanna test the frontend ( End-to-end testing, Unit testing, etc ). Simply call [Cypress](https://docs.cypress.io/guides/end-to-end-testing/writing-your-first-end-to-end-test) with the following command...

```
npm run test
```

## NOTE :

To minimize the number of annoying conflicts in this project ( please notice **_"minimize"_**, not **_"get rid of"_** ), I personally think we should consider the following notes :

1. This project makes use of [Typescript](https://www.typescriptlang.org/), if you hate it or is not familiar with it, simply rename any component that you have responsibility with to something that ends in `.jsx` instead of `tsx`, then have fun... But please remember, "Components" only, don't rename `main.tsx`.
2. If you are confused at the directory structure in `src/`, there is usually a `README.md` in all of those sub-directories, simply refer to them to understand more.
3. Please DO NOT USE global CSS !!! this is gonna be a heck of a conflict on the global CSS namespace, like one component is blue but when you switch to another page then return back to that component, now it's red. Please refer to [CSS Modules](https://create-react-app.dev/docs/adding-a-css-modules-stylesheet/) to understand how you can name your CSS `class` and `id` whatever you want with no conflict. Here is what i mean, say there are two team, each team has two members, then the directory structure should be something like this... ( please know that i made up the name of each member for the sake of demostration )

```
src
 └ assets
 └ components
    └ global
    └ layouts
    └ partial
     └ Team2Phase2Components
          └ VinhDTComponent
               └ index.tsx
               └ style.module.scss
          └ PhucNTComponent
               └ index.tsx
               └ style.module.scss
     └ Team1Phase1Components
          └ HoangTTComponent
               └ index.tsx
               └ style.module.scss
          └ LongTNComponent
               └ index.tsx
               └ style.module.scss
```

And for example, the content of `style.module.scss` is...

```scss
.main {
	background-color: black;
	font-size: 12px;
	padding: 0;
	margin: 0;
}
```

then `index.tsx` should be something like this...

```typescript
import style from "./style.module.scss";

export default function CoolComponent() {
	// style.main will generate a unique class

	return (
		<div class={style.main}>
			<h1>Very basic component</h1>
		</div>
	);
}
```
