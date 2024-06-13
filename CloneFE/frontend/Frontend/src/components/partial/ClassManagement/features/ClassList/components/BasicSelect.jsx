import * as React from 'react';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';

export default function BasicSelect({ items, selectedItem, setSelectedItem, valueKey })
{

    const handleChange = (event) =>
    {
        setSelectedItem(event.target.value);
    };

    return (
        <Box sx={{ width: "120px", height: "100px", marginLeft: '15px', marginTop: "10px" }}>
            <FormControl fullWidth>
                <Select
                    labelId="demo-simple-select-label"
                    id="demo-simple-select"
                    value={selectedItem || ''}
                    onChange={handleChange}
                    sx={{ height: '30px' }}
                    notched={false}
                >
                    {/* <MenuItem value={10}>Ten</MenuItem>
                    <MenuItem value={20}>Twenty</MenuItem>
                    <MenuItem value={30}>Thirty</MenuItem> */}
                    {items && items.length > 0 && items.map((item) => (
                        <MenuItem key={item[valueKey]} value={item[valueKey]}>
                            {item.name}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>
        </Box>
    );
}