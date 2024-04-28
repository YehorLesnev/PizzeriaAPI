"use client"
import TabsContainer from "@/components/tabs/tabs-container";
import {ITab} from "@/types";
import Customers from "@/components/tabs/admin-tabs/customers";
import Staff from "@/components/tabs/admin-tabs/staff";
import Items from "@/components/tabs/manager-tabs/items";
import Statistics from "@/components/tabs/manager-tabs/statistics/statistics";

const tabs: ITab[] = [
    {
        name: "Items",
        component: <Items />
    },
    {
        name: "Staff",
        component: <Staff/>
    },
    {
        name: "Customers",
        component: <Customers/>
    },
    {
        name: "Statistics",
        component: <Statistics/>
    }


]

const Manager = () => {
    return <div className={"w-[600px]"}>

        <h1 className={"pb-3"}>Manager Page</h1>


        <div className="flex flex-col">
            <TabsContainer tabs={tabs}/>
        </div>
    </div>
}

export default Manager