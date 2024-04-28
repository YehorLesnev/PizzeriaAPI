import {CiPizza} from "react-icons/ci";
import {cn} from "@/lib/utils";

interface LogoProps {
    size?: number
    isBordered?: boolean
}

const Logo = ({size, isBordered}: LogoProps) => {
    return <CiPizza size={size || 36} color={"orange"} className={cn("rounded-full border-orange-400  mr-1", {"border-2 border-orange-400": isBordered })}/>

}
export default Logo