import React from "react";
import ReactDOM from "react-dom/client";
// import '/index.css'
import { ChakraProvider } from "@chakra-ui/react";
import App from "./App";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import { Provider } from "react-redux";
import { store } from "./lib/redux/store";
import { BrowserRouter } from "react-router-dom";

const theme = createTheme({
  palette: {
    primary: {
      main: "#285D9A",
    },
    secondary: {
      main: "#DFDEDE",
    },
  },
  typography: {
    fontFamily: ["Inter"].join(","),
  },
  breakpoints: {
    values: {
      xs: 0,
      sm: 600,
      md: 900,
      lg: 1200,
      xl: 1536,
    },
  },
});

ReactDOM.createRoot(document.getElementById("root-fpt20242Uss_1")!).render(
  <ThemeProvider theme={theme}>
    <Provider store={store}>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </Provider>
  </ThemeProvider>
);
