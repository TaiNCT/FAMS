import Logo from "@/assets/public/logo@2x.png";
import UnigateLogo from "@/assets/public/image-2@2x.png";
const Navbar = () => {
    return (
        <div className='bg-[#2D3748] flex py-3 px-10 justify-between items-center text-white'>
            <img src={Logo} className='w-[92px]' alt='logo' />
            <div className='flex gap-10'>
                <div className='flex items-center'>
                    <div className='bg-[#0B2136]  flex items-center gap-3 py-1 px-4 rounded-full text-sm'>
                        <img src={UnigateLogo} alt="unigate" width="25px" /> uniGate
                    </div>
                </div> 
                <div className='flex gap-4'>
                    <div className='w-[43px] h-[43px] overflow-hidden rounded-full'>
                        <img src="/src/assets/LogoStManagement/mask-group.png" alt="" />
                    </div>
                    <div>
                        <div className='font-bold'>
                            Warrior Tran
                        </div>
                        <a href="https://www.amazon.com/">
                            Log out
                        </a>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Navbar