import {ReactNode, SVGProps} from "react";

export type IconSvgProps = SVGProps<SVGSVGElement> & {
  size?: number;
};


export interface ITab {
  name: string;
  component: JSX.Element;
}
export interface ITabs {
  tabs: ITab[]
}
