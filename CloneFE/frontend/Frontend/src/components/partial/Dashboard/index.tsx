import style from './style.module.scss';

function Dashboard() {
    return (
        <div className={style.dashboard}>
            <header className={style.header}>
                <h1>WELCOME TO FAMS</h1>
            </header>
            <div className={style.content}>
                <p>The cutting-edge solution tailored to enhance the efficiency of managing fresher academies at FPT Academy.</p>
            </div>
        </div>
    );
}

export { Dashboard };
