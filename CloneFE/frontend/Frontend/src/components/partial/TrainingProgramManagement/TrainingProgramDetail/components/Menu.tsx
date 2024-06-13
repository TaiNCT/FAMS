import * as React from "react";
import Box from "@mui/material/Box";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import Divider from "@mui/material/Divider";
import IconButton from "@mui/material/IconButton";
import { HiOutlineDotsHorizontal } from "react-icons/hi";
import Tooltip from "@mui/material/Tooltip";
import { MdDeleteOutline } from "react-icons/md";
import { Typography } from "@mui/material";
import style from "../style.module.scss";
import { MdOutlineModeEdit } from "react-icons/md";
import { HiOutlineDuplicate } from "react-icons/hi";
import { AiOutlineEyeInvisible } from "react-icons/ai";

const DetailMenu: React.FC = () => {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <React.Fragment>
      <Box sx={{ display: "flex", alignItems: "center", textAlign: "center" }}>
        {/* <Typography sx={{ minWidth: 50 }}>Contact</Typography> */}
        {/* <Typography sx={{ minWidth: 50 }}>Profile</Typography> */}
        <Tooltip title="Training Program settings">
          <IconButton
            onClick={handleClick}
            size="small"
            sx={{ ml: 2 }}
            // aria-controls={open ? "account-menu" : undefined}
            aria-haspopup="true"
            aria-expanded={open ? "true" : undefined}
          >
            <HiOutlineDotsHorizontal
              style={{ color: "white", marginRight: "1rem" }}
              size={42}
            />
          </IconButton>
        </Tooltip>
      </Box>
      <Menu
        anchorEl={anchorEl}
        id="account-menu"
        open={open}
        onClose={handleClose}
        onClick={handleClose}
        PaperProps={{
          elevation: 0,
          sx: {
            borderRadius: 4,
            overflow: "visible",
            filter: "drop-shadow(0px 2px 8px rgba(0,0,0,0.32))",
            mt: 1.5,
            // "& .MuiAvatar-root": {
            //   width: 32,
            //   height: 32,
            //   ml: -0.5,
            //   mr: 1,
            // },
            "&::before": {
              content: '""',
              display: "block",
              position: "absolute",
              top: 0,
              right: 14,
              width: 10,
              height: 10,
              bgcolor: "background.paper",
              transform: "translateY(-50%) rotate(45deg)",
              zIndex: 0,
            },
          },
        }}
        transformOrigin={{ horizontal: "right", vertical: "top" }}
        anchorOrigin={{ horizontal: "right", vertical: "bottom" }}
      >
        <Typography gutterBottom className={style.menuHeader}>
          <b>Manage</b>
        </Typography>
        <Divider />
        <div className={style.menuItems}>
          <MenuItem onClick={handleClose} className={style.test}>
            <MdOutlineModeEdit size={23} />{" "}
            <span>Edit program</span>
          </MenuItem>
          <MenuItem onClick={handleClose}>
            <HiOutlineDuplicate size={23} />
            <span>Duplicate program</span>
          </MenuItem>
          <MenuItem onClick={handleClose}>
            <AiOutlineEyeInvisible size={23} />
            <span>De-active program</span>
          </MenuItem>
          <MenuItem onClick={handleClose} style={{color: "black"}} disabled={true}>
            <MdDeleteOutline size={23} />
            <span>Delete program</span>
          </MenuItem>
        </div>
      </Menu>
    </React.Fragment>
  );
};

export default DetailMenu;
