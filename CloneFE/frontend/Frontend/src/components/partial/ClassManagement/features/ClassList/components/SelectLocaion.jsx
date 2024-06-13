import * as React from 'react';
import { useTheme } from '@mui/material/styles';
import Box from '@mui/material/Box';
import OutlinedInput from '@mui/material/OutlinedInput';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import Chip from '@mui/material/Chip';
import IconButton from '@mui/material/IconButton';
import CancelIcon from '@mui/icons-material/Cancel';

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
    PaperProps: {
        style: {
            maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
            width: 250,
        },
    },
};

function getStyles(name, personName, theme)
{
    return {
        fontWeight:
            personName.indexOf(name) === -1
                ? theme.typography.fontWeightRegular
                : theme.typography.fontWeightMedium,
    };
}

export default function MultipleSelectChip({ selectedItems, setSelectedItems, locationList })
{
    const theme = useTheme();

    const handleChange = (event) =>
    {
        const { target: { value } } = event;
        setSelectedItems(typeof value === 'string' ? value.split(',') : value);
    };

    const handleClear = (event) =>
    {
        event.stopPropagation();
        setSelectedItems([]);
    };

    return (
        <div>
            <div>
                <p style={{ color: '#2D3748', fontSize: '14', fontFamily: 'Inter', fontWeight: '700', wordWrap: 'break-word', marginLeft: '10px' }}>Class Location</p>
                <FormControl sx={{ marginLeft: '10px', width: '350px' }}>
                    <Select
                        labelId="demo-multiple-chip-label"
                        id="demo-multiple-chip"
                        multiple
                        value={selectedItems}
                        onChange={handleChange}
                        input={<OutlinedInput id="select-multiple-chip" label="" notched={false} endAdornment={
                            <IconButton onClick={handleClear} sx={{ marginRight: '10px' }}>
                                <CancelIcon />
                            </IconButton>
                        }
                        />
                        }
                        sx={{ height: '45px' }}
                        renderValue={(selected) => (
                            <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                                {selected.map((value) =>
                                {
                                    const location = locationList.find(item => item.id === value);
                                    return (
                                        <Chip key={value} label={location ? location.name : ''} />
                                    );
                                })}
                            </Box>
                        )}
                        MenuProps={MenuProps}
                    >
                        {locationList.map((item) => (
                            <MenuItem
                                key={item.id}
                                value={item.id}
                                style={getStyles(item.name, selectedItems.map(id => locationList.find(item => item.id === id).name), theme)}
                            >
                                {item.name}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>
            </div>
        </div>
    );
}
