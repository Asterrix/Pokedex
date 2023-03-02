import { createGlobalStyle } from "styled-components";
import { Color } from "./Color";
import Pokeball from "../assets/pokeball.svg";

export const GlobalStyles = createGlobalStyle`
  *, *:before, *:after {
    padding: 0;
    margin: 0;
    box-sizing: inherit;
  }

  /* Typography */
  html {
    font-size: 62.5%; /* base font-size */
    box-sizing: border-box;
  }

  body {
    font-family: 'Poppins', sans-serif; /* default font */
    font-size: 1.6rem;
    color: ${Color.Text}; /* default text color */
    line-height: 1.6; /* default line-height */
    background-image: url("${Pokeball}");
    background-size: calc(100vw - 10%) calc(100vh - 10%);
    background-position: center;
    background-repeat: no-repeat;
    background-attachment: fixed;
  }

  h1, h2, h3, h4, h5, h6 {
    font-weight: 600; /* headings weight */
  }

  /* Layout */
  #root {
    min-height: 100vh;
    display: flex;
    flex-direction: column;
    width: auto;
  }

  /* Media Queries */
  @media (max-width: 600px) {
    /* styles for small screens */
  }

  /* Transitions and Animations */
  .fade-in {
    animation: fade-in 0.5s ease-in;
  }

  @keyframes fade-in {
    from {
      opacity: 0;
    }
    to {
      opacity: 1;
    }
  }

  /* Print Styles */
  @media print {
    /* styles for print */
  }
`;