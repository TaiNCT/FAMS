// // cypress/integration/columnGroupingTable.spec.js

// describe("Column Grouping Table Tests", () => {
//   beforeEach(() => {
//     // Assuming your app is running on localhost:3000
//     cy.visit("http://localhost:3000");
//   });

//   it("should display the table", () => {
//     // Add assertions to ensure the table is rendered properly
//     cy.get(".big-page").should("exist");
//     cy.get(".inner-page").should("exist");
//     cy.get(".TableContainer-root").should("exist");
//     cy.get("table").should("exist");
//   });

//   it("should sort columns when clicked", () => {
//     // Replace 'Column Name' with the actual column name you want to test
//     cy.contains("th", "Column Name").click();
//     // Add assertions to ensure the column is sorted in ascending order
//     // You might want to check the first and last rows for proper sorting

//     // Click again to sort in descending order
//     cy.contains("th", "Column Name").click();
//     // Add assertions to ensure the column is sorted in descending order
//   });

//   it("should paginate the table", () => {
//     // Assuming your table has more than 10 rows
//     // Add assertions to check the initial page

//     // Change the rows per page to 25
//     cy.get("select").select("25");
//     // Add assertions to ensure the number of displayed rows is 25

//     // Go to the next page
//     cy.contains("button", "Next").click();
//     // Add assertions to check the content of the second page
//     // You might want to check the URL or some indicator for the page change

//     // Go back to the first page
//     cy.contains("button", "First").click();
//     // Add assertions to check if it's back to the initial page
//   });

//   // Add more tests based on your application's functionality
//   // For example, testing row expansion, checking content, etc.
// });
