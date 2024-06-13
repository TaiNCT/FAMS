import { Box } from '@chakra-ui/react'

import Profile from './Profile'

function Sidebar({fullName}) {
  return (
    <Box
      as="aside"
      flex={1}
      mr={{ base: 0, md: 5 }}
      mb={{ base: 5, md: 0 }}
      bg="white"
      rounded="md"
      borderColor="brand.light"
    >
      <Profile fullName={fullName} />
    </Box>
  )
}

export default Sidebar
