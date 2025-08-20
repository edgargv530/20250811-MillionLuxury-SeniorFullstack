import Header from '../organisms/Header'
import Menu from '../organisms/Menu'
import Footer from '../organisms/Footer';

export default function MainLayout({ children, onChangeMenu }) {
	const handleMenuSelect = (option) => {
		onChangeMenu(option);
	};

	return (
		<div className="container-fluid">
			<Header />

			{/* Área de trabajo */}
			<div className="row">
				{/* Menú lateral Izq. */}
				<Menu onSelect={handleMenuSelect} />

				{/* Cuerpo y carga de los componentes */}
				<main className="col ps-md-2 pt-2">
					<div className="row">
						<div className="col-12">
							{children}
						</div>
					</div>
				</main>
			</div>

			<Footer />
		</div>
	);
}
