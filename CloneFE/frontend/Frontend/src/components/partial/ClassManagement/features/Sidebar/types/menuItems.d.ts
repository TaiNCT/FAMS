export type MenuItem = {
  icon: React.ReactNode,
  label: string,
  link: string,
  children?: {
    label: string,
    link: string,
  }[]
}