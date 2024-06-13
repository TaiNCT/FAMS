import React from "react";
import { mount } from "cypress/react18";
import { ViewTraineeScore } from "./index";
import { BrowserRouter as Router } from "react-router-dom";

function assertCellValue(rowIndex, columnIndex, expectedValue) {
  cy.get(`tbody tr:nth-child(${rowIndex}) td:nth-child(${columnIndex})`).should(
    "contain",
    expectedValue
  );
}

describe("<ViewTraineeScore />", () => {
  beforeEach(() => {
    cy.intercept({
      method: "GET",
      url: "/api/Score",
    }).as("getScoreData");
  });

  it("renders and checks the fetched data", () => {
    cy.viewport(1920, 1080);
    mount(
      <Router>
        <ViewTraineeScore />
      </Router>
    );

    cy.wait("@getScoreData").then((interception) => {
      // Assert that the network request was intercepted and the response data was received
      expect(interception.response.statusCode).to.equal(200);
      expect(interception.response.body.data).to.be.an("array");
    });
  });
  it("sorts the table by clicking on column headers", () => {
    cy.viewport(1920, 1080);
    mount(
      <Router>
        <ViewTraineeScore />
      </Router>
    );
    cy.wait("@getScoreData");
    cy.get("th").contains("Full Name").find("svg").click();
    assertCellValue(1, 1, "Carmila Odesn");
    assertCellValue(15, 1, "William Wilson");

    cy.get("th").contains("Full Name").find("svg").click();
    assertCellValue(1, 1, "Đinh Thế Vinh");
    assertCellValue(15, 1, "Sophia Wilson");

    cy.get("th").contains("Account").find("svg").click();
    assertCellValue(1, 2, "CarmOD1k9");
    assertCellValue(15, 2, "HHSon2k1");

    cy.get("th").contains("Account").find("svg").click();
    assertCellValue(1, 2, "DTVinh12223");
    assertCellValue(15, 2, "example10");

    cy.get("th").contains("HTML").find("svg").click();
    assertCellValue(1, 3, "-");
    assertCellValue(15, 3, 97);
  });
});
