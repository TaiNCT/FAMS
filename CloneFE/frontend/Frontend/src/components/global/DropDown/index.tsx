// @ts-nocheck
import OutlinedInput from "@mui/material/OutlinedInput";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import ListItemText from "@mui/material/ListItemText";
import Select, { SelectChangeEvent } from "@mui/material/Select";
import Checkbox from "@mui/material/Checkbox";
import React, { useEffect } from "react";
import { RefObject, useRef } from "react";

interface IDropDown {
	defaultValue: string;
	options: string[];
	tag: string | null;
	ref_: RefObject<string>;
	onCallback?: () => void;
	disabled: boolean;
}

function DropDown({ disabled = false, ref_ = useRef<string>(""), defaultValue = "", options = [], tag = "", onCallback = (): void => {} }: IDropDown) {
	// Create a useRef(null) to reference the current component
	const currentRef = useRef(null);

	const ITEM_HEIGHT = 48;
	const ITEM_PADDING_TOP = 8;
	const [value, setValue] = React.useState<string[]>([defaultValue]);
	const onSelect = (event: SelectChangeEvent<typeof value>) => {
		setValue(event.target.value as string[]);
		ref_.current = event.target.value;
		onCallback(ref_.current);
	};

	// Assign default value
	useEffect(() => {
		ref_.current = value[0];

		// There is an UI problem when it comes to DropDOwn component which resides in
		// the <fieldset> and <legend> fields, so the following lines will fix it. This
		// was not a very significant issue but yet, it provides good UX for the end-users
		// let comp = document.querySelector(`#${style.indicator} fieldset span`);
		currentRef.current.querySelector("fieldset span").innerText = tag;
	}, []);

	return (
		<div ref={currentRef}>
			<FormControl sx={{ width: "100%" }}>
				<InputLabel id="demo-multiple-checkbox-label" disabled={disabled}>
					{tag}
				</InputLabel>
				<Select
					disabled={disabled}
					labelId="demo-multiple-checkbox-label"
					id="demo-multiple-checkbox"
					value={value}
					onChange={onSelect}
					input={<OutlinedInput label="Tag" />}
					renderValue={(selected) => selected}
					MenuProps={{
						PaperProps: {
							style: {
								maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
							},
						},
					}}
				>
					{options.map((name: string) => (
						<MenuItem key={name} value={name}>
							<Checkbox checked={value.indexOf(name) > -1} />
							<ListItemText primary={name} />
						</MenuItem>
					))}
				</Select>
			</FormControl>
		</div>
	);
}

export { DropDown };
