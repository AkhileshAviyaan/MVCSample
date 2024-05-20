$(document).ready(function () {
	loadDataTable();
});
function loadDataTable() {
	dataTable = $('#tblData').DataTable({
		"ajax": { url: '/admin/product/getall' },
		"columns": [
			{ data: 'title', "width": "15%" },
			{ data: 'description', "width": "15%" },
			{ data: 'isbn', "width": "10%" },
			{ data: 'author', "width": "10%" },
			{ data: 'listPrice', "width": "15%" },
			{
				data: 'id',
				"render": function (data) {
					return `<div class="w-75 btn-group" role="group">
					<a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2">Edit</a>
					<a href="/admin/product/delete?id=${data}" class="btn btn-danger" mx-2">Delete</a>
					</div>`
				}, "width": "25%"

			}
		]
	}
	);
}


