import NavigationIcons from './navigation-icons'
import { cn } from './utils'

type IProps = {
  isOpen: boolean
  onClick: () => void
}

export default function MenuButton({
  isOpen,
  onClick
}: IProps) {
  return (
    <div className={"p-2 flex justify-start items-center cursor-pointer"} onClick={onClick}>
      {isOpen ? <NavigationIcons icon='menu-close' /> : <NavigationIcons icon='menu-open' />}
    </div>
  )
}
