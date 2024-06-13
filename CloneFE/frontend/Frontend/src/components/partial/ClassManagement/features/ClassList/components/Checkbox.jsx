import * as React from 'react';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';

export default function CheckboxLabels({ labels, checkedBox, setCheckedBox })
{


    const handleChange = (event) =>
    {
        setCheckedBox({ ...checkedBox, [event.target.name]: event.target.checked });
    };

    React.useEffect(() =>
    {
        const parameters = Object.keys(checkedBox).filter((label) => checkedBox[label]);
    }, [checkedBox]);

    return (
        <FormGroup>
            {labels.map((label, index) => (
                <FormControlLabel
                    key={index}
                    control={
                        <Checkbox
                            checked={checkedBox[label]}
                            onChange={handleChange}
                            name={label}
                        />
                    }
                    label={label}
                />
            ))}
        </FormGroup>
    );
}
