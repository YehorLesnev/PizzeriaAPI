
import React, {useEffect, useState} from 'react';
import StatisticCard from "@/components/tabs/manager-tabs/statistics/statistics-card";
import {useAppDispatch} from "@/hooks/useStore";
import {getMonthSoldProductsCount} from "@/store/services/productService";
import {setTotalCount} from "@/store/features/product/productSlice";

const StatisticsCards = () => {
    const dispatch = useAppDispatch()
    const [burger, setBurger] = useState<number>(0)
    const [hotdog, setHotdog] = useState<number>(0)
    const [fries, setFries] = useState<number>(0)
    const [pizza, setPizza] = useState<number>(0)

    useEffect(() => {
        function getDateOneMonthAgo(): string {
            const currentDate = new Date();
            // Get the current month and year
            let currentMonth = currentDate.getMonth();
            let currentYear = currentDate.getFullYear();

            // Subtract one month
            if (currentMonth === 0) { // If it's January, go back to December of the previous year
                currentYear--;
                currentMonth = 11; // December
            } else {
                currentMonth--; // Otherwise, just go back one month
            }


            return `${currentYear}-${++currentMonth}-${currentDate.getDate()}`;
        }

        function  getCurrentDate(): string {
            const currentDate = new Date();

            return `${currentDate.getFullYear()}-${1 + currentDate.getMonth()}-${currentDate.getDate()}`;
        }

        const getPizzaCounter = async () => {
            const res = await getMonthSoldProductsCount({dateStart: getDateOneMonthAgo(), dateEnd:  getCurrentDate(), itemCategory: "Pizza"})

            if(res) setPizza(res)
        }
        const getBurgerCounter = async () => {
            const res = await getMonthSoldProductsCount({dateStart:  getDateOneMonthAgo(), dateEnd: getCurrentDate(), itemCategory: "Burger"})

            if(res) setBurger(res)
        }
        const getHotdogCounter = async () => {
            const res = await getMonthSoldProductsCount({dateStart: getDateOneMonthAgo(), dateEnd:  getCurrentDate(), itemCategory: "Hotdog"})

            if(res) setHotdog(res)
        }
        const getFriesCounter = async () => {
            const res = await getMonthSoldProductsCount({dateStart: getDateOneMonthAgo(), dateEnd:  getCurrentDate(), itemCategory: "Fries"})

            if(res)setFries(res)
        }

        getBurgerCounter()
        getPizzaCounter()
        getHotdogCounter()
        getFriesCounter()

    }, []);
    return (
        <div className={"flex items-center justify-center gap-10"}>
            <StatisticCard name={"Burgers"} quantity={burger} />
            <StatisticCard name={"Hotdogs"} quantity={hotdog} />
            <StatisticCard name={"Fries"} quantity={fries} />
            <StatisticCard name={"Pizza"} quantity={pizza} />
        </div>
    );
};

export default StatisticsCards;