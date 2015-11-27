var ymApp = angular.module('ymApp', ['ngRoute']);
ymApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            controller: 'DefaultCtrl',
            templateUrl: '/app/admin/views/index.html'
        }).when('/ArtList', {
            controller: 'ArtListCtrl',
            templateUrl: '/app/admin/views/artlist.html'
        }).when('/ArtList2', {
            controller: 'ArtListCtrl',
            templateUrl: '/app/admin/views/webuploader.html'
        }).when('/ArtList3', {
            controller: 'ArtListCtrl',
            templateUrl: '/app/admin/views/sweetalert.html'
        }).when('/ArtList4', {
            controller: 'ArtListCtrl',
            templateUrl: '/app/admin/views/formbasic.html'
        }).otherwise({
            redirectTo: '/'
        });
}]);
ymApp.run(function ($rootScope,loadingService) {
    $rootScope.$on('$routeChangeStart', function (ev, current, prev) {
        loadingService.load();
    })
    $rootScope.$on('$routeChangeSuccess', function () {
        loadingService.close();
    });
    $rootScope.$on('$routeChangeError', function (current, prev, rejection) {

        loadingService.close();
    });
});
ymApp.controller("DefaultCtrl", function ($scope) {

})
.controller("ArtListCtrl", function ($scope, loadingService) {
    $scope.loading = function () {
        loadingService.load();
        setTimeout(function () {
            loadingService.close();
        }, 2000)
    }
});
ymApp.directive('ymDelete', function ($http) {//需引入sweetalert插件
    return {
        restrict: 'E',
        link: function (scope, element, attrs) {
            element.css({ cursor: 'pointer' });
            element.on('click', function () {
                swal({
                    title: "您确定要删除这条信息吗",
                    text: "删除后将无法恢复，请谨慎操作！",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "删除",
                    cancelButtonText: "取消",
                    closeOnConfirm: false
                }, function () {
                    //执行删除
                    swal(attrs.delUrl + "删除成功！", "您已经永久删除了这条信息。", "success");
                })

            });
        }
    };
})
ymApp.directive('ymAlert', function () { 
    return {
        restrict:'AE',
        link: function (scope, element, attrs) {
            element.css({ cursor: 'pointer' });
            element.on('click', function () {
                sweetAlert(attrs.alertTitle, attrs.alertText, attrs.alertType);
            });
        }
    }
});
ymApp.factory('loadingService', function () {
    var service = {};
    service.load=function () {
      layer.load({ shade: 0.5 });
    }
    service.close = function () {
        layer.closeAll('loading'); //关闭加载层
    }
    return service;
});