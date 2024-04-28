"use client"
import TabsContainer from "@/components/tabs/tabs-container";
import {ITab} from "@/types";
import Customers from "@/components/tabs/admin-tabs/customers";
import Staff from "@/components/tabs/admin-tabs/staff";

const tabs: ITab[] = [
    {
        name: "Customers",
        component: <Customers/>
    },
    {
        name: "Staff",
        component: <Staff/>
    },

]

const Admin = () => {
    return <div className={"w-[600px]"}>

        <h1 className={"pb-3"}>Admin Page</h1>


        <div className="flex flex-col">
            <TabsContainer tabs={tabs}/>
        </div>
    </div>
}

export default Admin