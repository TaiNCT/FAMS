import NavigationIcons from '../components/icons/navigation-icons'

type IProps = {
  isOpen: boolean
}

export default function MenuItemToogle({
  isOpen,
}: IProps) {
  return (
    <div className="w-6 h-6 flex justify-center items-center cursor-pointer">
      {isOpen ? <NavigationIcons icon='nav-collapse' /> : <NavigationIcons icon='nav-default' />}
    </div>
  )
}
