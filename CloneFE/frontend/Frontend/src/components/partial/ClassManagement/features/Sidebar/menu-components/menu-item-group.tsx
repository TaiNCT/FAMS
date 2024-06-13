import { MenuItem } from '../types/menuItems'
import { cn } from '../menu-components/utils'
import { useState } from 'react'
import { Link, useLocation, useNavigate } from 'react-router-dom'
import MenuItemToogle from '../menu-components/menu-item-toogle'
import { useAppSelector } from '../hooks/useRedux'

type IProps = {
  menuItem: MenuItem,
}

export default function MenuItemGroup({
  menuItem,
}: IProps) {
  const { isOpen } = useAppSelector(state => state.sidebar)
  const location = useLocation()

  const [isExpanded, setIsExpanded] = useState(false)
  const navigate = useNavigate()


  const handleClickLink = (link: string, children: any) => {

    if ((link && children == undefined) || (!isOpen && link)) {
      navigate(link)
    } else if (children) {
      setIsExpanded(!isExpanded)
    }
  }

  return (
    <div key={menuItem.label}>
      <div className={
        cn("flex items-center gap-3 cursor-pointer py-2 px-2 rounded-sm",
          ((location.pathname === menuItem.link && menuItem.children == null) || (!isOpen && location.pathname === menuItem.link)) ? "bg-[#C5E9FF]" : ""
        )
      } onClick={() => handleClickLink(menuItem.link, menuItem.children)}>
        <div>{menuItem.icon}</div>
        <div className={cn(isOpen ? "flex-1 flex justify-between" : "hidden", "gap-3")}>
          <span>{menuItem.label}</span>
          {menuItem.children && <MenuItemToogle isOpen={!isExpanded} />}
        </div>
      </div>
      {
        (menuItem.children && (isOpen && isExpanded)) && (
          <div className="mt-2 space-y-[5px]">
            {
              menuItem.children.map((child, index) => (
                <Link to={child.link} key={child.label} className={
                  cn("flex items-center gap-3 p-[5px] pl-16 cursor-pointer rounded-sm",
                    location.pathname === child.link ? "bg-[#C5E9FF]" : "bg-[#ECF8FF]")
                }>
                  <span>{child.label}</span>
                </Link>
              ))
            }
          </div>
        )
      }
    </div>
  )
}
