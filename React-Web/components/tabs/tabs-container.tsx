"use client"
import {Tab, Tabs} from "@nextui-org/tabs";
import {Card, CardBody} from "@nextui-org/card";
import {ITab, ITabs} from "@/types";



const TabsContainer = ({tabs}: ITabs) => {
    return (
        <Tabs defaultSelectedKey={tabs[0].name}>{
            tabs && tabs.map(tab => (
                <Tab key={tab.name} title={tab.name}>
                    <Card  className={"min-w-[140vh]"}>
                        <CardBody>
                            {tab.component}
                        </CardBody>
                    </Card>
                </Tab>
            ))
        }</Tabs>
    )
}
export default TabsContainer